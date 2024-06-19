using Discount.Grpc.MappingConfigure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
MapsterConfig.RegisterMappings();

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(
    config =>
    {        
        config.RegisterServicesFromAssemblies(assembly);
        config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();


var sqliteDatabaseName = builder.Configuration.GetConnectionString("Database");
var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var projectPath = Directory.GetCurrentDirectory();
Console.WriteLine(path);
var dbPath = Path.Combine(projectPath + "//Data", sqliteDatabaseName);
Console.WriteLine(dbPath);
var connectionString = $"Data Source = {dbPath}";

builder.Services.AddDbContext<DiscountContext>(options =>
{
    options.UseSqlite(connectionString, sqliteOptions =>
    {
        //sqliteOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        sqliteOptions.CommandTimeout(30);
    })
        //.UseSqlServer(builder.Configuration.GetConnectionString("SqlDatabaseConnectionString"))
        //.UseLazyLoadingProxies()
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        .LogTo(Console.WriteLine, LogLevel.Information)
        ;
    if (!builder.Environment.IsProduction())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});


//builder.Services.AddSwaggerGen();

var app = builder.Build();

////app.UseMigration();//Use Extension class for auto Migration

app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
