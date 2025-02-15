using Microsoft.AspNetCore.Mvc;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.Travels.Api.Endpoints.Travels.GetAllTravels;

internal class GetAllTravelsRequest : PagedQuery
{
    [FromQuery] public string? SearchTerm { get; set; }

    public GetAllTravelsRequest()
    {
        SortOrder = "ASC";
        OrderBy = "Id";
    }
}