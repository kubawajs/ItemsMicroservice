using ItemsMicroservice.Core.Domain;

namespace ItemsMicroservice.Infrastructure.Repositories;

public interface IColorRepository
{
    Task<bool> ExistsAsync(string color, CancellationToken cancellationToken = default);
    Task<IEnumerable<Color>> GetAllAsync(CancellationToken cancellationToken = default);
}
