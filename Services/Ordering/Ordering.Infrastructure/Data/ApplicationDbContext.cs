using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using System.Reflection;

namespace Ordering.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : DbContext(options) , IApplicationDbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);

        builder.Entity<Customer>().HasQueryFilter(e => e.Del_Ind == 0);
        builder.Entity<Product>().HasQueryFilter(e => e.Del_Ind == 0);
        builder.Entity<Order>().HasQueryFilter(e => e.Del_Ind == 0);
        builder.Entity<OrderItem>().HasQueryFilter(e => e.Del_Ind == 0);
    }
}
