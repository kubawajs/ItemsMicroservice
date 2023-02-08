using ItemsMicroservice.Core.Domain;
using ItemsMicroservice.Infrastructure.MessageBroker.Bus;
using ItemsMicroservice.Infrastructure.Repositories;
using MediatR;

namespace ItemsMicroservice.Application.Items.UpdateItem;

public sealed class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, UpdateItemResponse?>
{
    private readonly IItemsRepository _itemsRepository;
    private readonly IEventBus _eventBus;

    public UpdateItemCommandHandler(IItemsRepository itemsRepository, IEventBus eventBus)
    {
        _itemsRepository = itemsRepository;
        _eventBus = eventBus;
    }

    public async Task<UpdateItemResponse?> Handle(UpdateItemCommand request, CancellationToken cancellationToken = default)
    {
        // TODO: check if color exists
        var item = new Item
        {
            Code = request.Code,
            Name = request.Name,
            Color = request.Color,
            Notes = request.Notes
        };

        await _itemsRepository.UpdateAsync(item, cancellationToken);
        await _eventBus.PublishAsync(new ItemUpdatedEvent(item.Code, item.Name), cancellationToken);

        return new UpdateItemResponse(item.Code, item.Name, item.Color, item.Notes);
    }
}