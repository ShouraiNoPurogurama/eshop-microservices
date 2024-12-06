using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services
    .AddApplicationServices() //Use case services
    .AddInfrastructureServices(builder.Configuration) //Database and model configuration services
    .AddApiServices(); //Routing-related services

var app = builder.Build();

//Configure the HTTP request pipeline
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.Run();