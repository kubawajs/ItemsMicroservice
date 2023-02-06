using MediatR;

namespace ItemsMicroservice.Application.Items.UpdateItem;

public sealed class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
{
    public Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
}