using ItemsMicroservice.Application.Exceptions;
using ItemsMicroservice.Core.Domain;
using ItemsMicroservice.Infrastructure.MessageBroker.Bus;
using ItemsMicroservice.Infrastructure.Repositories;
using MediatR;

namespace ItemsMicroservice.Application.Items.CreateItem;

public sealed class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, CreateItemResponse>
{
    private readonly IItemRepository _itemsRepository;
    private readonly IEventBus _eventBus;
    private readonly IColorRepository _colorRepository;

    public CreateItemCommandHandler(IItemRepository itemsRepository, IEventBus eventBus, IColorRepository colorRepository)
    {
        _itemsRepository = itemsRepository;
        _eventBus = eventBus;
        _colorRepository = colorRepository;
    }

    public async Task<CreateItemResponse> Handle(CreateItemCommand request, CancellationToken cancellationToken = default)
    {
        if(! await _colorRepository.ExistsAsync(request.Color, cancellationToken))
        {
            throw new InvalidColorException(request.Color);
        }

        var model = new Item
        {
            Code = request.Code,
            Name = request.Name,
            Color = request.Color,
            Notes = request.Notes
        };

        await _itemsRepository.CreateAsync(model, cancellationToken);
        await _eventBus.PublishAsync(new ItemCreatedEvent(model.Code, model.Name), cancellationToken);

        return new CreateItemResponse(request.Code, request.Name, request.Notes, request.Color);
    }
}
