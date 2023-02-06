using MediatR;

namespace ItemsMicroservice.Application.Items.GetItem;

public sealed class GetItemCommandHandler : IRequestHandler<GetItemQuery, GetItemResponse>
{
    Task<GetItemResponse> IRequestHandler<GetItemQuery, GetItemResponse>.Handle(GetItemQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
