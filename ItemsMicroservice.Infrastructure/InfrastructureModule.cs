using ItemsMicroservice.Infrastructure.Authentication;
using ItemsMicroservice.Infrastructure.Caching;
using ItemsMicroservice.Infrastructure.Database;
using ItemsMicroservice.Infrastructure.Exceptions;
using ItemsMicroservice.Infrastructure.MessageBroker;
using ItemsMicroservice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
                configuration.GetConnectionString("Database")));

        // Services
        services.AddScoped<IItemsRepository, ItemsRepository>();
        services.Decorate<IItemsRepository, CachedItemsRepository>();

        // Modules
        services.AddAuthenticationModule(configuration);
        services.AddCachingModule(configuration);
        services.AddExceptionsModule();
        services.AddMessageBrokerModule(configuration);
    }

    public static async Task UseInfrastructureAsync(this IApplicationBuilder app)
    {
        // Modules
        app.UseExceptionsModule();
        app.UseAuthenticationModule();

        // Database seed
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetService<ItemsMicroserviceDbContext>();
        if(context != null)
        {
            if((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }
            if(!context.Users.Any())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                await StaticDbInitializer.CreateUsersAsync(userManager);
            }
            if(!context.Colors.Any())
            {
                await StaticDbInitializer.CreateColorsAsync(context);
            }
            if(!context.Items.Any())
            {
                await StaticDbInitializer.CreateItemsAsync(context);
            }
        }
    }
}