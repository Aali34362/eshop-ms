namespace Discount.Grpc.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(100);
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    }

    ////public DateTime GetEarliestTeamMatch(Guid Id) => throw new NotImplementedException();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<Coupon>().HasQueryFilter(e => e.Del_Ind == 0);

        ////modelBuilder.Entity<Coupon>()
        ////    .HasNoKey().ToView("vw_Coupon");

        //For using Functions in SqlServer.
        //modelBuilder.HasDbFunction(typeof(DiscountContext)
        //    .GetMethod(nameof(GetEarliestTeamMatch), new[] { typeof(Guid) }))
        //    .HasName("GetEarliestTeamMatch");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
           .Entries<BaseRequest>()
           .Where(x =>
               x.State == EntityState.Modified
            || x.State == EntityState.Added
           );
        foreach (var entry in entries)
        {
            entry.Entity.Lst_Crtd_Date = DateTimeOffset.UtcNow.DateTime; ;
            entry.Entity.Lst_Crtd_User = "Admin";
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Crtd_Date = DateTimeOffset.UtcNow.DateTime; ;
                entry.Entity.Crtd_User = "Users";
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}

public class DiscountContextFactory : IDesignTimeDbContextFactory<DiscountContext>
{
    public DiscountContext CreateDbContext(string[] args)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);

        var projectPath = Directory.GetCurrentDirectory();

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        
        var sqliteDatabaseName = configuration.GetConnectionString("Database");
        var environment = configuration["Environment"] ?? "Development";

        ////var dbPath = Path.Combine(path, sqliteDatabaseName);
        var dbPath = Path.Combine(projectPath + "//Data", sqliteDatabaseName);

        var optionsBuilder = new DbContextOptionsBuilder<DiscountContext>();
        optionsBuilder.UseSqlite($"Data Source = {dbPath}")
            .UseLazyLoadingProxies()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .LogTo(Console.WriteLine, LogLevel.Information);

        if (environment != "Production")
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
        }

        return new DiscountContext(optionsBuilder.Options);
    }
}