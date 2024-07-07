namespace MassTransitMQ.MassTransit;

public static class Extension
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services, Assembly? assembly = null)
    {

        return services;
    }
}
