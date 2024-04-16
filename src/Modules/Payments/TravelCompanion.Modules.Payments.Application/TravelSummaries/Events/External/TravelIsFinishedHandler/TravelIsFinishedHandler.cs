﻿using TravelCompanion.Modules.Payments.Application.TravelSummaries.Commands;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Payments.Application.TravelSummaries.Events.External.TravelIsFinishedHandler;

public class TravelIsFinishedHandler : IEventHandler<TravelIsFinished>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public TravelIsFinishedHandler(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    public async Task HandleAsync(TravelIsFinished @event)
    {
        await _commandDispatcher.SendAsync(new GenerateTravelSummary(@event.travelId));
    }
}