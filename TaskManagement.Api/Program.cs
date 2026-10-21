using System.Reflection;
using Microsoft.AspNetCore.Http.HttpResults;
using TaskManagement.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpoints(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/swagger/v1/swagger.json");

    app.UseSwaggerUI(options => options
            .SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManagement API v1")
    );
}

app.MapGet("/health", () => new
{
    Status = "Healthy",
    Title = "TaskManagement API",
    ApiVersion = "v1"
});

app.UseAuthorization();

app.MapControllers();

app.Run();