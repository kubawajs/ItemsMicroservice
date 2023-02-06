using ItemsMicroservice.Infrastructure.Repositories;
using MediatR;

namespace ItemsMicroservice.Application.Items.GetItem;

public sealed class GetItemCommandHandler : IRequestHandler<GetItemQuery, GetItemResponse?>
{
    private readonly IItemsRepository _itemsRepository;

    public GetItemCommandHandler(IItemsRepository itemsRepository) => _itemsRepository = itemsRepository;

    public async Task<GetItemResponse?> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        var item = await _itemsRepository.GetByCodeAsync(request.Code, cancellationToken);
        return item != null ? new GetItemResponse(item.Code, item.Name, item.Notes, item.Color) : null;
    }
}
