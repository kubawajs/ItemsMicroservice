using MediatR;

namespace ItemsMicroservice.Application.Items.UpdateItem;

public sealed record UpdateItemCommand(string Code, string Name, string Notes, string Color) : IRequest;