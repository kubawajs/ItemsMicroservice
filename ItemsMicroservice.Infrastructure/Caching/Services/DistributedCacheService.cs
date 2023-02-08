using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ItemsMicroservice.Infrastructure.Caching.Services;

internal sealed class DistributedCacheService : IDistributedCacheService
{
    private readonly IDistributedCache _cache;

    public DistributedCacheService(IDistributedCache cache) => _cache = cache;

    public async Task<T?> GetAsync<T>(string cacheKey, CancellationToken cancellationToken = default)
    {
        var cachedItem = await _cache.GetAsync(cacheKey, cancellationToken);
        if (cachedItem != null)
        {
            return JsonSerializer.Deserialize<T?>(new ReadOnlySpan<byte>(cachedItem));
        }

        return default;
    }

    public async Task RemoveAsync(string cacheKey, CancellationToken cancellationToken = default)
    {
        await _cache.RemoveAsync(cacheKey, cancellationToken);
    }

    public async Task SetAsync<T>(string cacheKey, T cacheItem, CancellationToken cancellationToken = default)
    {
        if (cacheItem != null)
        {
            var cacheItemBytes = JsonSerializer.SerializeToUtf8Bytes(cacheItem);
            await _cache.SetAsync(cacheKey, cacheItemBytes, cancellationToken);
        }
    }
}
