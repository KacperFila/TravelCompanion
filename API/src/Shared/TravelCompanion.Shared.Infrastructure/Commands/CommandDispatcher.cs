﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Shared.Infrastructure.Commands
{
    internal sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            if (command is null)
            {
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command);
        }
    }
}