﻿using TravelCompanion.Modules.TravelPlans.Shared;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.DTO;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Emails;
using TravelCompanion.Shared.Abstractions.Events;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;
using TravelCompanion.Shared.Abstractions.Messaging;


namespace TravelCompanion.Modules.Travels.Core.Events.External.Handlers;

internal sealed class PlanAcceptedHandler : IEventHandler<PlanAccepted>
{
    private readonly ITravelRepository _travelRepository;
    private readonly ITravelPlansModuleApi _travelPlansModuleApi;
    private readonly IMessageBroker _messageBroker;
    private readonly IEmailSender _emailSender;
    private readonly IUsersModuleApi _usersModuleApi;
    private readonly IContext _context;
    private readonly Guid _userId;

    public PlanAcceptedHandler(ITravelRepository travelRepository, ITravelPlansModuleApi travelPlansModuleApi, IMessageBroker messageBroker, IEmailSender emailSender, IUsersModuleApi usersModuleApi, IContext context)
    {
        _travelRepository = travelRepository;
        _travelPlansModuleApi = travelPlansModuleApi;
        _messageBroker = messageBroker;
        _emailSender = emailSender;
        _usersModuleApi = usersModuleApi;
        _context = context;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(PlanAccepted @event)
    {
        var travelPlanPoints = await _travelPlansModuleApi.GetPlanTravelPointsAsync(@event.planId);
        var travelPlanReceipts = await _travelPlansModuleApi.GetPlanReceiptsAsync(@event.planId);

        var travelId = Guid.NewGuid();

        var travelPoints = travelPlanPoints.Select(x => AsTravelPoint(travelId, x)).ToList();
        var travelReceipts = AsTravelReceipts(travelId, travelPlanReceipts);

        var travel = new Travel
        {
            Id = travelId,
            OwnerId = @event.ownerId,
            ParticipantIds = @event.participants,
            AdditionalCosts = travelReceipts,
            AdditionalCostsValue = Money.Create(@event.additionalCostsValue),
            Title = @event.title,
            Description = @event.description,
            From = @event.from,
            To = @event.to,
            AllParticipantsPaid = false,
            IsFinished = false,
            Ratings = new List<TravelRating>(),
            RatingValue = null,
            TravelPoints = travelPoints,
            TotalCostsValue = Money.Create(@event.totalCost)
        };

        await _travelRepository.AddAsync(travel);
        await _messageBroker.PublishAsync(new TravelFromPlanCreated(@event.planId));

        var usersEmails = await _usersModuleApi.GetUsersEmails(@event.participants.ToList());
        await _emailSender.SendEmailAsync(new AcceptedPlanEmailDTO(), usersEmails);
    }

    private static TravelPoint AsTravelPoint(Guid travelId, TravelPlans.Domain.Plans.Entities.TravelPoint travelPoint)
    {
        var travelPointId = Guid.NewGuid();

        return new TravelPoint(
            travelPointId,
            travelPoint.PlaceName,
            travelId,
            AsTravelPointReceipts(travelPointId, travelPoint.Receipts),
            travelPoint.TotalCost);
    }

    private static List<Receipt> AsTravelReceipts(Guid travelId, List<TravelPlans.Domain.Plans.Entities.Receipt> receipts)
    {
        var receiptsResult = new List<Receipt>();

        foreach (var receipt in receipts)
        {
            receiptsResult.Add(
                Receipt.Create(
                    receipt.ReceiptOwnerId,
                    travelId,
                    null,
                    receipt.Amount,
                    receipt.Description,
                    receipt.ReceiptParticipants));
        }

        return receiptsResult;
    }

    private static List<Receipt> AsTravelPointReceipts(Guid pointId, List<TravelPlans.Domain.Plans.Entities.Receipt> receipts)
    {
        var receiptsResult = new List<Receipt>();

        foreach (var receipt in receipts)
        {
            receiptsResult.Add(
                Receipt.Create(
                    receipt.ReceiptOwnerId,
                    null,
                    pointId,
                    receipt.Amount,
                    receipt.Description,
                    receipt.ReceiptParticipants));
        }

        return receiptsResult;
    }
}