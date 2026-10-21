using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TaskManagement.Domain.Enitity;

namespace TaskManagement.Application;

public class MapGetProjects : IEndpoints
{
    public record CreateProjectsDto(string Name, string Description);

    public void MapEndpoint(IEndpointRouteBuilder app, CancellationToken cancellationToken)
    {
        app.MapGet("/api/projects", async (MapCreateProjects.CreateProjectsDto dto, AppDbContext db) =>
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return Results.BadRequest("Некорректные данные пользователя.");
            }
            
            var project = Projects.CreateProject();
            
            db.Projects.Add(project);
            await db.SaveChangesAsync();
            
            return Results.CreatedAtRoute("GetUserById", new { id = project.Id }, project);
        });
    }
}