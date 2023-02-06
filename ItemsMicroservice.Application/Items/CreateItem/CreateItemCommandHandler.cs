using MediatR;

namespace ItemsMicroservice.Application.Items.CreateItem;

public sealed class CreateItemCommandHandler : IRequestHandler<CreateItemCommand>
{
    public Task<Unit> Handle(CreateItemCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
