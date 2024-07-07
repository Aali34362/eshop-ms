var builder = WebApplication.CreateBuilder(args);

//Application Services
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(
    config =>
    {
        //RegisterServices From Assemblies means we are gong to register all the services from this project into the mediator class library.
        config.RegisterServicesFromAssemblies(assembly);
        config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });
////builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(assembly);

//Data Services
builder.Services.AddMarten(
    opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("Database")!);
        ////opts.AutoCreateSchemaObjects();
        opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    })
    .UseLightweightSessions();
////.InitializeWith()

////if (builder.Environment.IsDevelopment())
////    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IBasketRepository,BasketRepository>();
builder.Services.Decorate<IBasketRepository,CachedBasketRepository>();
////builder.Services.AddScoped<IBasketRepository>(provider => {
////    var basketRepository = provider.GetRequiredService<BasketRepository>();
////    return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
////});
builder.Services.AddStackExchangeRedisCache( options => {
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    //options.InstanceName = "Basket";
});

//GRPC Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opts =>
{
    opts.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
}).ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        return handler;
    });

//Async Communication Services
builder.Services.AddRabbitMQMessageBroker(builder.Configuration);

//Cross-Cutting Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddWatchDogServices(opt =>
{
    opt.IsAutoClear = true;
    opt.SetExternalDbConnString = builder.Configuration.GetConnectionString("Database")!;
    opt.DbDriverOption = WatchDogDbDriverEnum.PostgreSql;    
});
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket");
        c.RoutePrefix = string.Empty;  // To serve the Swagger UI at the app's root
    });
}
app.MapCarter();

app.UseHealthChecks("/health",
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }
    );
////app.UseWatchDogExceptionLogger();

app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = "admin";
    opt.WatchPagePassword = "Qwerty@123";

    //Optional
    ////opt.Blacklist = "Test/testPost, api/auth/login"; //Prevent logging for specified endpoints
    ////opt.Serializer = WatchDogSerializerEnum.Newtonsoft; //If your project use a global json converter
    ////opt.CorsPolicy = "MyCorsPolicy";
    ////opt.UseOutputCache = true;
    ////opt.UseRegexForBlacklisting = true;

});

app.UseExceptionHandler(
    option =>
    {

    }
    );

app.Run();
