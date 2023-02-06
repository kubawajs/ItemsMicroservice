using ItemsMicroservice.Core.Domain;
using ItemsMicroservice.Infrastructure.Repositories;
using MediatR;

namespace ItemsMicroservice.Application.Items.UpdateItem;

public sealed class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, UpdateItemResponse?>
{
    private readonly IItemsRepository _itemsRepository;

    public UpdateItemCommandHandler(IItemsRepository itemsRepository) => _itemsRepository = itemsRepository;

    public async Task<UpdateItemResponse?> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var item = new Item
        {
            Code = request.Code,
            Name = request.Name,
            Color = request.Color,
            Notes = request.Notes
        };

        await _itemsRepository.UpdateAsync(item, cancellationToken);

        return new UpdateItemResponse(item.Code, item.Name, item.Color, item.Notes);
    }
}