using ItemsMicroservice.Core.Domain;
using ItemsMicroservice.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ItemsMicroservice.Infrastructure.Repositories;

internal sealed class ItemsRepository : IItemsRepository
{
    private readonly ItemsMicroserviceDbContext _context;

    public ItemsRepository(ItemsMicroserviceDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Item item, CancellationToken cancellationToken = default)
    {
        // Update system properties
        item.CreatedAt = DateTime.UtcNow;
        item.UpdatedAt= DateTime.UtcNow;

        await _context.Items.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Item?> GetByCodeAsync(string code, CancellationToken cancellationToken = default) => 
        await _context.Items
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Code == code, cancellationToken: cancellationToken);

    public async Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Items
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

    public async Task UpdateAsync(Item item, CancellationToken cancellationToken = default)
    {
        // Update system properties
        item.UpdatedAt = DateTime.UtcNow;

        _context.Update(item);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Item>> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _context.Items
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default) =>
        await _context.Items.CountAsync(cancellationToken);
}
