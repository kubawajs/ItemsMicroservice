using ItemsMicroservice.Core.Domain;
using ItemsMicroservice.Infrastructure.Database;
using ItemsMicroservice.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ItemsMicroservice.Infrastructure.Repositories;

internal sealed class ItemsRepository : IItemsRepository
{
    private readonly ItemsMicroserviceDbContext _context;

    public ItemsRepository(ItemsMicroserviceDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Item item, CancellationToken cancellationToken)
    {
        await _context.Items.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Item?> GetByCodeAsync(string code, CancellationToken cancellationToken) => 
        await _context.Items.FindAsync(code, cancellationToken);

    public async Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken)
    {
        // TODO: pagination
        return await _context.Items.ToListAsync();
    }

    public async Task UpdateAsync(Item item, CancellationToken cancellationToken)
    {
        var itemToUpdate = await _context.Items.FindAsync(item);
        if(itemToUpdate == null)
        {
            throw new ItemNotFoundException(item);
        }

        itemToUpdate.Name = item.Name;
        itemToUpdate.Color = item.Color;
        itemToUpdate.Notes = item.Notes;

        _context.Update(item);
    }
}
