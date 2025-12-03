using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.RealTime.Travels;

public interface ITravelsRealTimeService
{
    Task SendTravelUpdate(List<Guid> participantUserIds, object travel);
    Task SendActiveTravelChanged(Guid userId, Guid travelId);
}