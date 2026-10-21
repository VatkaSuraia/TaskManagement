using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TaskManagement.Domain.Enitity;

namespace TaskManagement.Application;

public class MapCreateProjects : IEndpoints
{
    public record CreateProjectsDto(string Name, string Description);
    public record ResponseProjectsDto(Guid Id, string Name, string Description, DateTime CreatedAt);

    public void MapEndpoint(IEndpointRouteBuilder app, CancellationToken cancellationToken)
    {
        app.MapPost("/api/projects", async (CreateProjectsDto dto, AppDbContext db) =>
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
        
        app.MapGet("/api/projects", async (AppDbContext db) =>
        {
            var listP = db.Projects
                .Select(p => new ResponseProjectsDto(
                    p.id, p.name,
                    p.description, p.createdAt
                    )).ToListAsync();
            
            if(listP is null)
                return Results.NotFound("Проекты не найдены");
            
            
            return Results.Ok(listP);
        });
        
        app.MapGet("/api/projects{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var listP = await db.Projects.FindAsync(id);
            
            if(listP is null)
                return Results.NotFound("Проект не найдены");

            var response = new ResponseProjectsDto(
                listP.id,
                listP.name,
                listP.description,
                listP.createdAt);
            
            return Results.Ok(response);
        });
    }
}
