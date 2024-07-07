using MassTransit;
using Microsoft.Extensions.Configuration;

namespace MassTransitMQ.MassTransit;

public static class Extension
{
    public static IServiceCollection AddRabbitMQMessageBroker(this IServiceCollection services, IConfiguration configuration,Assembly? assembly = null)
    {
        services.AddMassTransit(
            config =>
            {
                config.SetKebabCaseEndpointNameFormatter();
                if (assembly != null)
                    config.AddConsumers(assembly);
                config.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(configuration["RabbitMessageBroker:Host"]!), host =>
                        {
                            host.Username(configuration["RabbitMessageBroker:UserName"]!);
                            host.Password(configuration["RabbitMessageBroker:Password"]!);
                        });
                    configurator.ConfigureEndpoints(context);
                });
            });
        return services;
    }
}
