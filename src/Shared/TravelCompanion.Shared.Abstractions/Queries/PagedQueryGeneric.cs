namespace TravelCompanion.Shared.Abstractions.Queries;

public abstract class PagedQueryGeneric<T> : PagedQuery, IPagedQuery<Paged<T>>, IQuery<T>
{
}
