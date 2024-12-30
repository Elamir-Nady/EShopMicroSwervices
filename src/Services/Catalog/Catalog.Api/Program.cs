using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Catalog.Api.DataContext;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

using static System.Net.Mime.MediaTypeNames;
var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connection)
);
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddHealthChecks();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//builder.Services.AddMarten(opts =>
//{
//    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
//}).UseLightweightSessions();



var app = builder.Build();
if (app.Environment.IsDevelopment())
{
   
}
app.MapHealthChecks("/health",new HealthCheckOptions
{
    ResponseWriter =UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapCarter();
app.UseExceptionHandler(option => { });
app.Run();
