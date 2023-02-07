using ItemsMicroservice.Infrastructure.Database;
using ItemsMicroservice.Infrastructure.Repositories;
using ItemsMicroservice.Infrastructure.Services;
using ItemsMicroservice.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ItemsMicroservice.Infrastructure;

public static class InfrastructureModule
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ItemsMicroserviceDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("Database")));

        // Services registration
        services.AddScoped<IItemsRepository, ItemsRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IIdentityService, IdentityService>();

        // Authentication
        services.ConfigureIdentity();
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddJwtAuthentication(configuration);
        services.AddAuthorization(x => x.AddPolicy(Constants.Policies.RequireAdminRole, p => p.RequireRole(Constants.Users.Roles.Admin)));
    }

    public static async Task UseInfrastructureAsync(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetService<ItemsMicroserviceDbContext>();
        if(context != null)
        {
            await context.Database.MigrateAsync();
            if(!context.Users.Any())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                await StaticDbInitializer.CreateUsersAsync(userManager);
            }
        }
    }

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.GetSection(JwtSettings.SectionName).Bind(jwtSettings);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
        });

        return services;
    }

    private static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ItemsMicroserviceDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            // Sign In
            options.SignIn.RequireConfirmedAccount = false;

            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });

        services.ConfigureApplicationCookie(options =>
        {
            // Cookie settings
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            options.SlidingExpiration = true;
        });

        return services;
    }
}