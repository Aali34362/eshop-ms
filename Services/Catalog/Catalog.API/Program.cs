var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
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

builder.Services.AddMarten(
    opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("Database")!);
        ////opts.AutoCreateSchemaObjects();
    })
    .UseLightweightSessions();
////.InitializeWith()

////if (builder.Environment.IsDevelopment())
////    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCarter();
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
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog");
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
