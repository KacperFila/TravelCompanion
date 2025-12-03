using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using TravelCompanion.Shared.Infrastructure.Modules;

namespace TravelCompanion.Bootstrapper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger(); // Minimal logger for startup errors

            try
            {
                Log.Information("Starting TravelCompanion Bootstrapper...");

                var host = CreateHostBuilder(args).Build();

                Log.Information("Host built. Running application...");
                host.Run();

                Log.Information("Application shutdown cleanly.");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Bootstrapper terminated unexpectedly");
            }
            finally
            {
                Log.Information("Flushing and closing log...");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddEnvironmentVariables();
                    Log.Information("Environment variables added to configuration.");
                })
                .UseSerilog((context, services, configuration) =>
                {
                    var env = context.HostingEnvironment.EnvironmentName;
                    var elasticUriRaw = context.Configuration["ElasticConfiguration:Uri"];

                    configuration
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("Environment", env)
                        .WriteTo.Console();

                    if (env == "Development" &&
                        !string.IsNullOrWhiteSpace(elasticUriRaw) &&
                        Uri.TryCreate(elasticUriRaw, UriKind.Absolute, out var elasticUri))
                    {
                        configuration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticUri)
                        {
                            IndexFormat = "travelcompanion-logs-{0:yyyy.MM.dd}",
                            AutoRegisterTemplate = true,
                            NumberOfShards = 2,
                            NumberOfReplicas = 1
                        });

                        Log.Information("Elasticsearch logging enabled for Development environment.");
                        Log.Information("Elastic URI: {ElasticUri}", elasticUri);
                    }
                    else
                    {
                        Log.Information("Elasticsearch logging not configured. Environment: {Environment}, URI: {Uri}", env, elasticUriRaw ?? "<null>");
                    }

                    configuration.ReadFrom.Configuration(context.Configuration);

                    Log.Information("Using environment: {Environment}", env);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.Listen(IPAddress.Any, 5000);
                        Log.Information("Kestrel configured to listen on port 5000.");
                    })
                    .UseStartup<Startup>();
                })
                .ConfigureModules();
    }
}
