using ItemsMicroservice.Core.Domain;
using ItemsMicroservice.Infrastructure.Caching.Services;

namespace ItemsMicroservice.Infrastructure.Repositories;

internal sealed class CachedColorRepository : IColorRepository
{
    private const string _cachePrefix = "colors";

    private readonly IColorRepository _decoratedRepository;
    private readonly IDistributedCacheService _distributedCacheService;

    public CachedColorRepository(IColorRepository decoratedRepository, IDistributedCacheService distributedCacheService)
    {
        _decoratedRepository = decoratedRepository;
        _distributedCacheService = distributedCacheService;
    }

    public async Task<bool> ExistsAsync(string color, CancellationToken cancellationToken = default)
    {
        var key = $"{_cachePrefix}-exists-{color}";
        var cachedColorExists = await _distributedCacheService.GetAsync<string>(key, cancellationToken);
        if(!string.IsNullOrWhiteSpace(cachedColorExists))
        {
            return bool.Parse(cachedColorExists);
        }
        
        var colorExists = await _decoratedRepository.ExistsAsync(color, cancellationToken);
        await _distributedCacheService.SetAsync(key, colorExists.ToString(), cancellationToken);

        return colorExists;
    }

    public async Task<IEnumerable<Color>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var key = $"{_cachePrefix}-all";
        var cachedColors = await _distributedCacheService.GetAsync<IEnumerable<Color>?>(key, cancellationToken);
        if(cachedColors!= null)
        {
            return cachedColors;
        }

        var colors = await _decoratedRepository.GetAllAsync(cancellationToken);
        if(colors != null)
        {
            await _distributedCacheService.SetAsync(key, colors, cancellationToken);
        }

        return colors ?? new List<Color>();
    }
}
