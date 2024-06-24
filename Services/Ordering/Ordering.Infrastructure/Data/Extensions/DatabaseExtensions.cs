namespace Ordering.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();
    }
}
