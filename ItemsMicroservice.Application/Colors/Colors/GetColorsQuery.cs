using MediatR;

namespace ItemsMicroservice.Application.Colors.Colors;

public sealed record GetColorsQuery : IRequest<IEnumerable<GetColorResponse>>;
