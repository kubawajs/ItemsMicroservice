using ItemsMicroservice.Core.Domain;

namespace ItemsMicroservice.Infrastructure.Repositories;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Item>> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Item?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task UpdateAsync(Item item, CancellationToken cancellationToken = default);
    Task CreateAsync(Item item, CancellationToken cancellationToken = default);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);
}
