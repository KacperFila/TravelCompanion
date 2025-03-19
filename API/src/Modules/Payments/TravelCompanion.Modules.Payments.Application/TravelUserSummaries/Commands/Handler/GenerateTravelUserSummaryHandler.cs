using TravelCompanion.Modules.Payments.Domain.Payments.Exceptions;
using TravelCompanion.Modules.Travels.Shared;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.Payments.Application.TravelUserSummaries.Commands.Handler;

public class GenerateTravelUserSummaryHandler : ICommandHandler<GenerateTravelUserSummary>
{
    private readonly ITravelsModuleApi _travelModuleApi;
    private readonly IContext _context;
    private readonly Guid _userId;
    public GenerateTravelUserSummaryHandler(ITravelsModuleApi travelModuleApi, IContext context)
    {
        _travelModuleApi = travelModuleApi;
        _context = context;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(GenerateTravelUserSummary command)
    {
        var travel = await _travelModuleApi.GetTravelInfo(command.travelId);

        if (travel is null)
        {
            throw new TravelNotFoundException(command.travelId);
        }

        var userReceipts = travel.ParticipantsCosts
            .Where(x => x.ReceiptParticipants
                .Contains(_userId))
            .ToList();

        //var paymentRecords = userReceipts.SelectMany(x => x.ReceiptParticipants.Where(y => y != _userId),
        //    (receipt, guid) => PaymentRecord.Create(_userId, ))


        //var userSummary = TravelUserSummary.Create()
    }
}