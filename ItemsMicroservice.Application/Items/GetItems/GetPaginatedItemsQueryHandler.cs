using ItemsMicroservice.Application.Filters;
using ItemsMicroservice.Application.Items.GetItem;
using ItemsMicroservice.Infrastructure.Repositories;
using MediatR;

namespace ItemsMicroservice.Application.Items.GetItems;

public sealed class GetPaginatedItemsQueryHandler : IRequestHandler<GetPaginatedItemsQuery, GetPaginatedItemsResponse>
{
    private readonly IItemRepository _itemsRepository;

    public GetPaginatedItemsQueryHandler(IItemRepository itemsRepository) => _itemsRepository = itemsRepository;

    public async Task<GetPaginatedItemsResponse> Handle(GetPaginatedItemsQuery request, CancellationToken cancellationToken = default)
    {
        var paginationFilter = new PaginationFilter(request?.Page, request?.PageSize);
        var items = await _itemsRepository.GetPaginatedAsync(paginationFilter.Page, paginationFilter.PageSize, cancellationToken);
        var itemsResponse = items.Select(item => new GetItemResponse(item.Code, item.Name, item.Notes, item.Color));
        var totalPages = (int) Math.Ceiling(await _itemsRepository.GetTotalCountAsync(cancellationToken) / (double) paginationFilter.PageSize);

        return new GetPaginatedItemsResponse(itemsResponse, totalPages, paginationFilter.Page, paginationFilter.PageSize);
    }
}
