using ItemsMicroservice.Infrastructure.Caching.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsMicroservice.Infrastructure.Caching;
public static class CachingModule
{
    public static void AddCachingModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDistributedCacheService, DistributedCacheService>();
        services.AddStackExchangeRedisCache(options => options.Configuration = configuration.GetConnectionString("Redis"));
    }
}
