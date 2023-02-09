using ItemsMicroservice.Core.Domain;
using ItemsMicroservice.Infrastructure.Caching.Services;

namespace ItemsMicroservice.Infrastructure.Repositories;

internal sealed class CachedItemsRepository : IItemsRepository
{
    private string _cacheKeyPrefix = "items";

    private readonly IItemsRepository _decoratedRepository;
    private readonly IDistributedCacheService _cacheService;

    public CachedItemsRepository(IItemsRepository decoratedRepository, IDistributedCacheService cacheService)
    {
        _decoratedRepository = decoratedRepository;
        _cacheService = cacheService;
    }

    public async Task CreateAsync(Item item, CancellationToken cancellationToken = default)
    {
        await _decoratedRepository.CreateAsync(item, cancellationToken);
        await _cacheService.RemoveAsync($"{_cacheKeyPrefix}-all", cancellationToken);
    }

    public async Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var key = $"{_cacheKeyPrefix}-all";
        var cachedItems = await _cacheService.GetAsync<IEnumerable<Item>>(key, cancellationToken);
        if (cachedItems != null)
        {
            return cachedItems;
        }

        var items = await _decoratedRepository.GetAllAsync(cancellationToken);
        if (items != null)
        {
            await _cacheService.SetAsync(key, items, cancellationToken);
        }

        return items ?? new List<Item>();
    }

    public async Task<Item?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var key = $"{_cacheKeyPrefix}-{code}";
        var cachedItem = await _cacheService.GetAsync<Item?>(key, cancellationToken);
        if (cachedItem != null)
        {
            return cachedItem;
        }

        var item = await _decoratedRepository.GetByCodeAsync(code, cancellationToken);
        if (item != null)
        {
            await _cacheService.SetAsync(key, item, cancellationToken);
        }

        return item;
    }

    public async Task UpdateAsync(Item item, CancellationToken cancellationToken = default)
    {
        await _decoratedRepository.UpdateAsync(item, cancellationToken);
        await _cacheService.RemoveAsync($"{_cacheKeyPrefix}-{item.Code}", cancellationToken);
    }

    public async Task<IEnumerable<Item>> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var key = $"{_cacheKeyPrefix}-page{page}-size{pageSize}";
        var cachedItems = await _cacheService.GetAsync<IEnumerable<Item>>(key, cancellationToken);
        if (cachedItems != null)
        {
            return cachedItems;
        }

        var items = await _decoratedRepository.GetPaginatedAsync(page, pageSize, cancellationToken);
        if (items != null)
        {
            await _cacheService.SetAsync(key, items, cancellationToken);
        }

        return items ?? new List<Item>();
    }

    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        var key = $"{_cacheKeyPrefix}-count";
        var cachedTotalCount = await _cacheService.GetAsync<int?>(key, cancellationToken);
        if (cachedTotalCount != null)
        {
            return cachedTotalCount.Value;
        }

        var totalCount = await _decoratedRepository.GetTotalCountAsync(cancellationToken);
        await _cacheService.SetAsync(key, totalCount, cancellationToken);

        return totalCount;
    }
}