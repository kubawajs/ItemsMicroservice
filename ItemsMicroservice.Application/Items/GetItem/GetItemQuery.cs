using MediatR;

namespace ItemsMicroservice.Application.Items.GetItem;

public sealed record GetItemQuery(string Code) : IRequest<GetItemResponse>;
