using ItemsMicroservice.Core.Domain;

namespace ItemsMicroservice.Infrastructure.Repositories;
public interface IItemsRepository
{
    Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken);
    Task<Item?> GetByCodeAsync(string code, CancellationToken cancellationToken);
    Task UpdateAsync(Item item, CancellationToken cancellationToken);
    Task CreateAsync(Item item, CancellationToken cancellationToken);
}
