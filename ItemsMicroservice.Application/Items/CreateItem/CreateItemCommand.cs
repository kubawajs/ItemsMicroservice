using MediatR;

namespace ItemsMicroservice.Application.Items.CreateItem;

public sealed record CreateItemCommand(string Code, string Name, string Notes, string Color) : IRequest<CreateItemResponse>;
