﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Shared.Infrastructure.Postgres.Decorators
{
    [Decorator]
    internal class TransactionalCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
    {
        private readonly ICommandHandler<T> _handler;
        private readonly IServiceProvider _serviceProvider;
        private readonly UnitOfWorkTypeRegistry _unitOfWorkTypeRegistry;

        public TransactionalCommandHandlerDecorator(ICommandHandler<T> handler, IServiceProvider serviceProvider,
            UnitOfWorkTypeRegistry unitOfWorkTypeRegistry)
        {
            _handler = handler;
            _serviceProvider = serviceProvider;
            _unitOfWorkTypeRegistry = unitOfWorkTypeRegistry;
        }

        public async Task HandleAsync(T command)
        {
            var unitOfWorkType = _unitOfWorkTypeRegistry.Resolve<T>();
            if (unitOfWorkType is null)
            {
                await _handler.HandleAsync(command);
                return;
            }

            var unitOfWork = (IUnitOfWork)_serviceProvider.GetRequiredService(unitOfWorkType);
            await unitOfWork.ExecuteAsync(() => _handler.HandleAsync(command));
        }
    }
}