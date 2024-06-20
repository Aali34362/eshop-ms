var builder = WebApplication.CreateBuilder(args);

//Add services to the container

//---------
//Infrastructure - EF Core
//Application - MediatR
//API - Carter, HealthChecks,....

//builder.Services
//    .AddApplicationServices()
//    .AddInfrastructureServices(builder.Configuration)
//    .AddWebServices();
//-------------------

var app = builder.Build();

//Configure the HTTP request pipeline

app.Run();
