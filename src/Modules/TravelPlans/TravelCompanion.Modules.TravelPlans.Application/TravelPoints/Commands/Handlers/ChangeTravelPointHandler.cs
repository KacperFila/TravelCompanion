﻿using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

public class ChangeTravelPointHandler : ICommandHandler<ChangeTravelPoint>
{
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly IPlanRepository _planRepository;
    private readonly ITravelPointSuggestionsRepository _travelPointSuggestionsRepository;
    private readonly IContext _context;
    private readonly Guid _userId;

    public ChangeTravelPointHandler(ITravelPointRepository travelPointRepository, IContext context, IPlanRepository planRepository, ITravelPointSuggestionsRepository travelPointSuggestionsRepository)
    {
        _travelPointRepository = travelPointRepository;
        _context = context;
        _planRepository = planRepository;
        _travelPointSuggestionsRepository = travelPointSuggestionsRepository;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(ChangeTravelPoint command)
    {
        var doesTravelPointExist = await _travelPointRepository.ExistAsync(command.travelPointId);

        if (!doesTravelPointExist)
        {
            throw new TravelPointNotFoundException(command.travelPointId);
        }

        var travelPoint = await _travelPointRepository.GetAsync(command.travelPointId);
        var travelPlan = await _planRepository.GetAsync(travelPoint.TravelPlanId);

        if (!(travelPlan.OwnerId == _userId || travelPlan.ParticipantIds.Contains(_userId)))
        {
            throw new UserNotAllowedToChangeTravelPointException();
        }

        if (!travelPoint.IsAccepted)
        {
            throw new CouldNotModifyNotAcceptedTravelPointException();
        }

        var suggestion = TravelPointChangeSuggestion.Create(command.travelPointId, _userId, command.placeName);

        await _travelPointSuggestionsRepository.AddAsync(suggestion);

        //TODO create accept/reject suggestions, refactor code (endpoints, create policies)
    }
}