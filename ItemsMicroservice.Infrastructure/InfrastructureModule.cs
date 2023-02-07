using ItemsMicroservice.Infrastructure.Database;
using ItemsMicroservice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsMicroservice.Infrastructure;
public static class InfrastructureModule
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ItemsMicroserviceDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("Database"))); // TODO: to settings object

        services.AddScoped<IItemsRepository, ItemsRepository>();
    }

    public static async Task UseInfrastructureAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetService<ItemsMicroserviceDbContext>();
        if(context != null)
        {
            await context.Database.MigrateAsync();
        }
    }
}