﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Infrastructure.Postgres.Decorators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Shared.Infrastructure.Postgres
{
    public static class Extensions
    {
        public static Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, IPagedQuery query,
            CancellationToken cancellationToken = default)
        {
            return data.PaginateAsync(query.Page, query.Results, cancellationToken);
        }

        public static async Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, int page, int results,
            CancellationToken cancellationToken = default)
        {
            if (page <= 0)
            {
                page = 1;
            }

            results = results switch
            {
                <= 0 => 10,
                > 100 => 100,
                _ => results
            };

            var totalResults = await data.CountAsync();
            // var totalPages = totalResults <= results ? 1 : (int)Math.Floor((double)totalResults / results);
            var totalPages = (int)Math.Ceiling(totalResults / (double)results);

            var result = await data.Skip((page - 1) * results).Take(results).ToListAsync(cancellationToken);

            return new Paged<T>(result, page, results, totalPages, totalResults);
        }

        internal static IServiceCollection AddPostgres(this IServiceCollection services)
        {
            var options = services.GetOptions<PostgresOptions>("postgres");
            services.AddSingleton(options);
            services.AddSingleton(new UnitOfWorkTypeRegistry());
            
            return services;
        }

        public static IServiceCollection AddTransactionalDecorators(this IServiceCollection services)
        {
            services.TryDecorate(typeof(ICommandHandler<>), typeof(TransactionalCommandHandlerDecorator<>));

            return services;
        }

        public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
        {
            var options = services.GetOptions<PostgresOptions>("postgres");
            services.AddDbContext<T>(x => x.UseNpgsql(options.ConnectionString));

            return services;
        }

        public static IServiceCollection AddUnitOfWork<TUnitOfWork, TImplementation>(this IServiceCollection services)
            where TUnitOfWork : class, IUnitOfWork where TImplementation : class, TUnitOfWork
        {
            services.AddScoped<TUnitOfWork, TImplementation>();
            services.AddScoped<IUnitOfWork, TImplementation>();

            using var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>().Register<TUnitOfWork>();

            return services;
        }
    }
}