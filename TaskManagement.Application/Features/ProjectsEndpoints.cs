using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TaskManagement.Domain.Enitity;

namespace TaskManagement.Application.Features;


public class CreateProjectEndpoint : IEndpoints
{
    public record Request(string Name, string Description);

    public void Map(IEndpointRouteBuilder app, CancellationToken cancellationToken)
    {
        app.MapPost("/api/projects", async (Request dto, IProjectRepository db) =>
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return Results.BadRequest("Некорректные данные: имя проекта обязательно.");
            }

            var project = Project.CreateProject(dto.Name, dto.Description, null);
            
            await db.AddAsync(project, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            
            return Results.CreatedAtRoute("GetProjectById", new { id = project.Id }, project); //для фронта
        });
    }
}

public class GetAllProjectsEndpoint : IEndpoints
{
    public record Response(Guid Id, string Name, string Description, DateTime CreatedAt);

    public void Map(IEndpointRouteBuilder app, CancellationToken cancellationToken)
    {
        app.MapGet("/api/projects", async (IProjectRepository db) =>
        {
            var listP = await db.GetAllAsync(cancellationToken);
            
            var response = listP.Select(p => new Response(p.Id, p.Name, p.Description, p.CreatedAt))
                .ToList(); //из-за него вернется пустой лист если нету
            
            return Results.Ok(response);
        });
    }
}

public class GetProjectByIdEndpoint : IEndpoints
{
    public record Response(Guid? Id, string? Name, string? Description, DateTime? CreatedAt);

    public void Map(IEndpointRouteBuilder app, CancellationToken cancellationToken)
    {
        app.MapGet("/api/projects/{id:guid}", async (Guid id, IProjectRepository db) =>
            {
                var project = await db.GetByIdAsync(id, cancellationToken);

                if (project is null)
                {
                    var response = new Response(
                        null, null, null, null
                        );
                    return Results.Ok(response);
                    //без 400 как и говорили
                }

                else
                {
                    var response = new Response(
                        project.Id,
                        project.Name,
                        project.Description,
                        project.CreatedAt
                    );
                    return Results.Ok(response);
                }
            })
            .WithName("GetProjectById");//для фронта вроде надо
    }
}


