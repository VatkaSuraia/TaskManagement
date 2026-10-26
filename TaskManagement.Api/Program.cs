using System.Reflection;
using Microsoft.AspNetCore.Http.HttpResults;
using TaskManagement.Application;
using TaskManagement.Infrastructure;

using TaskManagement.Api;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer(); 

builder.Services.AddOpenApi(); 

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddEndpoints(typeof(TaskManagement.Application.DependencyInjection).Assembly);

var app = builder.Build();

app.UseMapEndpoints(new CancellationToken(), null); 


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/swagger/v1/swagger.json");

    app.UseSwaggerUI(options => options
            .SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManagement API v1")
    );
}

app.UseAuthorization();

app.MapControllers();

app.Run();