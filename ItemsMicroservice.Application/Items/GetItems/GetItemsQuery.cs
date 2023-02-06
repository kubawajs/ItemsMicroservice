using ItemsMicroservice.Application.Items.GetItem;
using MediatR;

namespace ItemsMicroservice.Application.Items.GetItems;

public sealed record GetItemsQuery : IRequest<IEnumerable<GetItemResponse>>;
