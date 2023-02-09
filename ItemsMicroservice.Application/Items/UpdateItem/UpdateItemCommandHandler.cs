using ItemsMicroservice.Application.Exceptions;
using ItemsMicroservice.Core.Domain;
using ItemsMicroservice.Infrastructure.MessageBroker.Bus;
using ItemsMicroservice.Infrastructure.Repositories;
using MediatR;

namespace ItemsMicroservice.Application.Items.UpdateItem;

public sealed class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, UpdateItemResponse?>
{
    private readonly IItemRepository _itemsRepository;
    private readonly IColorRepository _colorRepository;
    private readonly IEventBus _eventBus;

    public UpdateItemCommandHandler(IItemRepository itemsRepository, IEventBus eventBus, IColorRepository colorRepository)
    {
        _itemsRepository = itemsRepository;
        _eventBus = eventBus;
        _colorRepository = colorRepository;
    }

    public async Task<UpdateItemResponse?> Handle(UpdateItemCommand request, CancellationToken cancellationToken = default)
    {
        if (!await _colorRepository.ExistsAsync(request.Color, cancellationToken))
        {
            throw new InvalidColorException(request.Color);
        }

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