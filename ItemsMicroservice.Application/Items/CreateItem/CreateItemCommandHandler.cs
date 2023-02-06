using ItemsMicroservice.Core.Domain;
using ItemsMicroservice.Infrastructure.Repositories;
using MediatR;

namespace ItemsMicroservice.Application.Items.CreateItem;

public sealed class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, CreateItemResponse>
{
    private readonly IItemsRepository _itemsRepository;

    public CreateItemCommandHandler(IItemsRepository itemsRepository) => _itemsRepository = itemsRepository;

    public async Task<CreateItemResponse> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        // TODO: DTO
        var model = new Item
        {
            Code = request.Code,
            Name = request.Name,
            Color = request.Color,
            Notes = request.Notes
        };

        await _itemsRepository.CreateAsync(model, cancellationToken);

        return new CreateItemResponse(request.Code, request.Name, request.Notes, request.Color);
    }
}
