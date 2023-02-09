using ItemsMicroservice.Application.Items.GetItem;
using MediatR;

namespace ItemsMicroservice.Application.Items.GetItems;

public sealed record GetPaginatedItemsQuery(int? Page, int? PageSize) : IRequest<GetPaginatedItemsResponse>;
