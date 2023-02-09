using ItemsMicroservice.Core.Domain;
using ItemsMicroservice.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ItemsMicroservice.Infrastructure.Repositories;

internal sealed class ColorRepository : IColorRepository
{
    private readonly ItemsMicroserviceDbContext _context;

    public ColorRepository(ItemsMicroserviceDbContext context) => _context = context;

    public async Task<bool> ExistsAsync(string color, CancellationToken cancellationToken)
    {
        color = color.ToLowerInvariant();
        return await _context.Colors
            .AnyAsync(c => c.Name == color, cancellationToken);
    }

    public async Task<IEnumerable<Color>> GetAllAsync(CancellationToken cancellationToken) =>
        await _context.Colors
            .OrderBy(x => x.Id)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
}
