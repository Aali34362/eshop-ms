namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionSettings = configuration.GetConnectionString("Database");

        //Add services to the container
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp,opts) => 
        {
            opts.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            opts.UseSqlServer(connectionSettings); 
        });
        ///services.AddScoped<IApplicationDbContext,ApplicationDbContext>();
        return services;
    }
}