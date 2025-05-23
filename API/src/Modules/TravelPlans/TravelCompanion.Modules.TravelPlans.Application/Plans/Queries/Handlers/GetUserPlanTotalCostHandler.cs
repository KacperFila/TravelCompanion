using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Queries.Handlers;

internal sealed class GetUserPlanTotalCostHandler : IQueryHandler<GetUserPlanTotalCost, Money>
{
    private readonly IPlanRepository _planRepository;
    private readonly IReceiptRepository _receiptRepository;
    private readonly IContext _context;
    private readonly Guid _userId;

    public GetUserPlanTotalCostHandler(IPlanRepository planRepository, IContext context,
        IReceiptRepository receiptRepository)
    {
        _planRepository = planRepository;
        _context = context;
        _receiptRepository = receiptRepository;
        _userId = _context.Identity.Id;
    }

    public async Task<Money> HandleAsync(GetUserPlanTotalCost query)
    {
        var plan = await _planRepository.GetAsync(query.PlanId);

        if (plan is null)
        {
            throw new PlanNotFoundException(query.PlanId);
        }

        if (!plan.Participants.Any(x => x.ParticipantId == _userId))
        {
            throw new UserDoesNotParticipateInPlanException(_userId, query.PlanId);
        }

        var planReceipts = await _receiptRepository.BrowseForPlanAsync(query.PlanId);
        var pointIds = plan.TravelPlanPoints.Select(x => x.Id).ToList();
        var userPointReceipts = new List<Receipt>();

        foreach (var pointId in pointIds)
        {
            var pointReceipts = await _receiptRepository.BrowseForPointAsync(pointId);
            userPointReceipts.AddRange(
                pointReceipts
                    .Where(x => x.ReceiptParticipants
                        .Contains(_userId))
                );
        }

        var userReceipts = planReceipts.Concat(userPointReceipts);

        var userPartOfAmount = userReceipts.Sum(receipt =>
            CalculateUserReceiptCost(receipt.Amount.Amount, receipt.ReceiptParticipants.Count));

        var totalUserPlanCost = Money.Create(userPartOfAmount);

        return totalUserPlanCost;
    }

    private static decimal CalculateUserReceiptCost(decimal amount, int participantsCount)
        => amount / participantsCount;
}
