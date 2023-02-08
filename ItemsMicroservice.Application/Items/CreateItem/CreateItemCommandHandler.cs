using ItemsMicroservice.Core.Domain;
using ItemsMicroservice.Infrastructure.MessageBroker.Bus;
using ItemsMicroservice.Infrastructure.Repositories;
using MediatR;

namespace ItemsMicroservice.Application.Items.CreateItem;

public sealed class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, CreateItemResponse>
{
    private readonly IItemsRepository _itemsRepository;
    private readonly IEventBus _eventBus;

    public CreateItemCommandHandler(IItemsRepository itemsRepository, IEventBus eventBus)
    {
        _itemsRepository = itemsRepository;
        _eventBus = eventBus;
    }

    public async Task<CreateItemResponse> Handle(CreateItemCommand request, CancellationToken cancellationToken = default)
    {
        // TODO: check if color exists
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
