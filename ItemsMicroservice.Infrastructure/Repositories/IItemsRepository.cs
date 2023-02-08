using ItemsMicroservice.Core.Domain;

namespace ItemsMicroservice.Infrastructure.Repositories;
public interface IItemsRepository
{
    Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Item?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task UpdateAsync(Item item, CancellationToken cancellationToken = default);
    Task CreateAsync(Item item, CancellationToken cancellationToken = default);
}
