namespace ItemsMicroservice.Infrastructure.Services;
public interface IDistributedCacheService
{
    Task<T?> GetAsync<T>(string cacheKey, CancellationToken cancellationToken = default);
    Task RemoveAsync(string cacheKey, CancellationToken cancellationToken = default);
    Task SetAsync<T>(string cacheKey, T cacheItem, CancellationToken cancellationToken = default);
}
