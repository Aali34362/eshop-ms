namespace Ordering.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddWebService(this IServiceCollection services)
    {

        return services;
    }

    public static WebApplication UseWebService(this WebApplication webApplication)
    {

        return webApplication;
    }
}

