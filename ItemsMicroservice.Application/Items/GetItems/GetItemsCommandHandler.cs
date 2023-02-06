using ItemsMicroservice.Application.Items.GetItem;
using ItemsMicroservice.Infrastructure.Repositories;
using MediatR;

namespace ItemsMicroservice.Application.Items.GetItems;

public sealed class GetItemsCommandHandler : IRequestHandler<GetItemsQuery, IEnumerable<GetItemResponse>>
{
    private readonly IItemsRepository _itemsRepository;

    public GetItemsCommandHandler(IItemsRepository itemsRepository) => _itemsRepository = itemsRepository;

    public async Task<IEnumerable<GetItemResponse>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await _itemsRepository.GetAllAsync(cancellationToken);
        return items.Select(item => new GetItemResponse(item.Code, item.Name, item.Notes, item.Color));
    }
}