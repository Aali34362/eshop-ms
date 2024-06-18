var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(
    config =>
    {        
        config.RegisterServicesFromAssemblies(assembly);
        config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

//builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
