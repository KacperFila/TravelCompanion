using System.Collections.Generic;

namespace TravelCompanion.Shared.Abstractions.Queries;

public interface IPagedQuery : IQuery
{
    int Page { get; set; }
    int Results { get; set; }
    string OrderBy { get; set; }
    string SortOrder { get; set; }
}

public interface IPagedQuery<T> : IPagedQuery, IQuery<T>
{
}