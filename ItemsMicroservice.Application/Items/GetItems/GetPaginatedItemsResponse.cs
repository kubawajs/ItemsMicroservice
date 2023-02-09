using ItemsMicroservice.Application.Items.GetItem;

namespace ItemsMicroservice.Application.Items.GetItems;

public sealed record GetPaginatedItemsResponse(IEnumerable<GetItemResponse> Items, int TotalPages, int CurrentPage, int PageSize);