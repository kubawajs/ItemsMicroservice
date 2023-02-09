using ItemsMicroservice.Core.Domain;
using Microsoft.Extensions.Logging;

namespace ItemsMicroservice.Infrastructure.Repositories;

internal sealed class LoggingColorRepository : IColorRepository
{
    private readonly IColorRepository _colorRepository;
    private readonly ILogger<LoggingColorRepository> _logger;

    public LoggingColorRepository(ILogger<LoggingColorRepository> logger, IColorRepository colorRepository)
    {
        _colorRepository = colorRepository;
        _logger = logger;
    }

    public async Task<bool> ExistsAsync(string color, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Checking if color {color} exists in database...");
        var colorExists = await _colorRepository.ExistsAsync(color, cancellationToken);
        _logger.LogInformation($"Color {color} exists: {colorExists}");

        return colorExists;
    }

    public async Task<IEnumerable<Color>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving colors from database...");
        var colors = await _colorRepository.GetAllAsync(cancellationToken);
        _logger.LogInformation($"Successfully retrieved {colors.Count()} from database.");

        return colors;
    }
}
