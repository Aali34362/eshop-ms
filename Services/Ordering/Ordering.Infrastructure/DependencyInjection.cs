namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionSettings = configuration.GetConnectionString("Database");
        services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(connectionSettings));
        //services.AddScoped<IApplicationDbContext,ApplicationDbContext>();
        return services;
    }
}