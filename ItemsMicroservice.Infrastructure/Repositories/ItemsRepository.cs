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
        await _context.Items.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Item?> GetByCodeAsync(string code, CancellationToken cancellationToken = default) => 
        await _context.Items.AsNoTracking().FirstOrDefaultAsync(item => item.Code == code);

    public async Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        // TODO: pagination
        return await _context.Items.AsNoTracking().ToListAsync();
    }

    public async Task UpdateAsync(Item item, CancellationToken cancellationToken = default)
    {
        _context.Update(item);
        await _context.SaveChangesAsync();
    }
}
