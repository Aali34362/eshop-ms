using System.Reflection;

namespace Ordering.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Customer>().Property(c=>c.Name).IsRequired().HasMaxLength(100);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
