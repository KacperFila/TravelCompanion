namespace TravelCompanion.Shared.Abstractions.Queries;

public abstract class PagedBase
{
    public int CurrentPage { get; set; }
    public int ResultsPerPage { get; set; }
    public int TotalPages { get; set; }
    public long TotalResults { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;

    protected PagedBase()
    {
    }

    protected PagedBase(int currentPage, int resultsPerPage,
        int totalPages, long totalResults)
    {
        CurrentPage = currentPage;
        ResultsPerPage = resultsPerPage;
        TotalPages = totalPages;
        TotalResults = totalResults;
    }
}