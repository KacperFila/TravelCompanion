using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.DTO;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Services.Abstractions;
using TravelCompanion.Shared.Abstractions.Events;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Travels.Core.Events.External.Handlers;

internal sealed class PlanAcceptedHandler : IEventHandler<PlanAccepted>
{
    private readonly ITravelRepository _travelRepository;
    public PlanAcceptedHandler(ITravelRepository travelRepository)
    {
        _travelRepository = travelRepository;
    }

    public async Task HandleAsync(PlanAccepted @event)
    {

        var travel = new Travel
        {
            Id = Guid.NewGuid(),
            OwnerId = @event.ownerId,
            ParticipantIds = @event.participants,
            AdditionalCosts = @event.additionalCosts.ToList(),
            AdditionalCostsValue = Money.Create(@event.additionalCostsValue),
            Title = @event.title,
            Description = @event.description,
            From = @event.from,
            To = @event.to,
            AllParticipantsPaid = false,
            IsFinished = false,
            Ratings = new List<TravelRating>(),
            RatingValue = null
        };

        await _travelRepository.AddAsync(travel);
    }
}