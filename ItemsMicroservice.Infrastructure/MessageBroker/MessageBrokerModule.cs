using ItemsMicroservice.Infrastructure.MessageBroker.Bus;
using ItemsMicroservice.Infrastructure.MessageBroker.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ItemsMicroservice.Infrastructure.MessageBroker;

public static class MessageBrokerModule
{
    public static void AddMessageBrokerModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageBrokerSettings>(configuration.GetSection(MessageBrokerSettings.SectionName));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var settings = context.GetRequiredService<MessageBrokerSettings>();
                configurator.Host(new Uri(settings.Host), host =>
                {
                    host.Username(settings.Username);
                    host.Password(settings.Password);
                });
            });
        });

        services.AddScoped<IEventBus, EventBus>();
    }
}
