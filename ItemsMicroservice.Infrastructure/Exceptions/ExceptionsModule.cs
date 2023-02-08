using ItemsMicroservice.Infrastructure.Exceptions.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsMicroservice.Infrastructure.Exceptions;
public static class ExceptionsModule
{
    public static void AddExceptionsModule(this IServiceCollection services)
    {
        services.AddSingleton<ExceptionsMiddleware>();
    }

    public static void UseExceptionsModule(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionsMiddleware>();
    }
}
