namespace ItemsMicroservice.Application.Filters;

public sealed record PaginationFilter
{
    private const int _minPageSize = 10;
    private const int _maxPageSize = 1000;

    public int Page { get; init; }
    public int PageSize { get; init; }

    public PaginationFilter(int? page, int? pageSize)
    {
        Page = page > 0 ? page.Value : 1;
        PageSize = !pageSize.HasValue || pageSize.Value < _minPageSize
            ? _minPageSize
            : PageSize > _maxPageSize
            ? _maxPageSize
            : pageSize.Value;
    }
}
