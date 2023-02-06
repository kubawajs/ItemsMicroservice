using ItemsMicroservice.Infrastructure.Database;
using ItemsMicroservice.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsMicroservice.Infrastructure;
public static class InfrastructureModule
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ItemsMicroserviceDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Database")));

        services.AddScoped<IItemsRepository, ItemsRepository>();
    }
}
