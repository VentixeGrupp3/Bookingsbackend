using Azure.Messaging.ServiceBus;
using Business.Services;
using Data.Contexts;
using Data.Repositories;
using EventService.Protos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddGrpc();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("BookingDB")));
builder.Services.AddGrpcClient<EventGrpcService.EventGrpcServiceClient>(o =>
{
    o.Address = new Uri("https://ventixegrupp3-ajgbb3hpacbxbhf4.swedencentral-01.azurewebsites.net/");
});

builder.Services.AddSingleton(x =>
{
    var connectionString = builder.Configuration.GetConnectionString("ServiceBus");
    return new ServiceBusClient(connectionString);
});

builder.Services.AddScoped<QueueService>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Ventixe BookingsBackend API Documentation"
    });
});

var app = builder.Build();


app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ventixe Bookings Microservice API");
    options.RoutePrefix = string.Empty;
});
app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "");

app.Run();
