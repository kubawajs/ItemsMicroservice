using ItemsMicroservice.Infrastructure.Repositories;
using MediatR;

namespace ItemsMicroservice.Application.Colors.Colors;

public sealed class GetColorsQueryHandler : IRequestHandler<GetColorsQuery, IEnumerable<GetColorResponse>>
{
    private readonly IColorRepository _colorRepository;

    public GetColorsQueryHandler(IColorRepository colorRepository) => _colorRepository = colorRepository;

    public async Task<IEnumerable<GetColorResponse>> Handle(GetColorsQuery request, CancellationToken cancellationToken = default)
    {
        var colors = await _colorRepository.GetAllAsync(cancellationToken);
        return colors.Select(color => new GetColorResponse(color.Id, color
            .Name));
    }
}