using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsMicroservice.Application;
public static class ApplicationModule
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ApplicationModule));
    }
}
