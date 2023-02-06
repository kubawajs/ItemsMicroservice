using ItemsMicroservice.Application.Items.GetItem;
using MediatR;

namespace ItemsMicroservice.Application.Items.GetItems;

public sealed class GetItemsCommandHandler : IRequestHandler<GetItemsQuery, IEnumerable<GetItemResponse>>
{
    Task<IEnumerable<GetItemResponse>> IRequestHandler<GetItemsQuery, IEnumerable<GetItemResponse>>.Handle(GetItemsQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
}