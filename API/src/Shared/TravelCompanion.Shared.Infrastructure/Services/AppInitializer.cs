﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TravelCompanion.Shared.Infrastructure.Services
{
    internal class AppInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AppInitializer> _logger;

        public AppInitializer(IServiceProvider serviceProvider, ILogger<AppInitializer> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext));

            using var scope = _serviceProvider.CreateScope();

            _logger.LogInformation($"===== Start initializing db migrations... =====");

            foreach (var dbContextType in dbContextTypes)
            {
                var dbContext = scope.ServiceProvider.GetService(dbContextType) as DbContext;
                if (dbContext is null)
                {
                    continue;
                }

                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(cancellationToken);

                if (pendingMigrations.Count() is not 0)
                {
                    _logger.LogInformation($"===== Initialize migrations for {dbContextType.Name} =====");
                    _logger.LogInformation($"===== Applying {pendingMigrations.Count()} migrations for {dbContextType.Name} =====");
                    await dbContext.Database.MigrateAsync(cancellationToken);
                }
            }

            _logger.LogInformation($"===== No migrations to be applied, database schema is up to date =====");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}