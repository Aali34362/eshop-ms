var builder = WebApplication.CreateBuilder(args);

//Add services to the container

//---------
//Infrastructure - EF Core
//Application - MediatR
//API - Carter, HealthChecks,....

builder.Services
    .AddApplicationService()
    .AddInfrastructureService(builder.Configuration)
    .AddWebService(builder.Configuration);
//-------------------

var app = builder.Build();

//Configure the HTTP request pipeline
app.UseWebService();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order");
        c.RoutePrefix = string.Empty;  // To serve the Swagger UI at the app's root
    });
    ////await app.InitialiseDatabaseAsync();
}
app.Run();
