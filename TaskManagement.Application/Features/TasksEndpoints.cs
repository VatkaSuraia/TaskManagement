using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TaskManagement.Domain.Enitity;


namespace TaskManagement.Application.Features;

public class CreateTaskItemEndpoint : IEndpoints
{
    public record Request(string Title, string? Description, Status? Status, Guid Projects);

    public void Map(IEndpointRouteBuilder app, CancellationToken cancellationToken)
    {
        app.MapPost("/api/tasks", async (Request dto, ITaskRepository db) =>
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return Results.BadRequest("Некорректные данные: заголовок задачи обязателен.");
            

            if (dto.Projects == Guid.Empty)
                return Results.BadRequest("Нельзя создать задачу для несуществующего проекта.");
            
            
            var taskItem = TaskItem.CreateTaskItem(
                dto.Title, 
                dto.Description, 
                dto.Status ?? Status.Todo, 
                dto.Projects
            );
            
            await db.AddAsync(taskItem, cancellationToken); 
            await db.SaveChangesAsync(cancellationToken);
            
            return Results.CreatedAtRoute("GetTaskItemById", new { id = taskItem.Id }, taskItem); 
        });
    }
}

public class GetAllTaskItemsEndpoint : IEndpoints
{
    public record Response(Guid Id, string Title, string? Description, string Status, Guid Projects, DateTime CreatedAt);

    public void Map(IEndpointRouteBuilder app, CancellationToken cancellationToken)
    {
        app.MapGet("/api/tasks", async (Guid? projectId, Status? status, ITaskRepository db) =>
        {
            
            var listT = await db.GetAllAsync(cancellationToken);
            listT.Select(t => new Response(
                    t.Id, 
                    t.Title, 
                    t.Description, 
                    t.Status.ToString() ,
                    t.Projects, 
                    t.CreatedAt
                ))
                .ToList(); 
            
            return Results.Ok(listT);
        });
    }
}

public class GetTaskItemByIdEndpoint : IEndpoints
{
    public record Response(Guid? Id, string? Title, string? Description, string? Status, Guid? Projects, DateTime? CreatedAt);

    public void Map(IEndpointRouteBuilder app, CancellationToken cancellationToken)
    {
        app.MapGet("/api/tasks/{id:guid}", async (Guid id, ITaskRepository db) =>
            {
                var taskItem = await db.GetByIdAsync(id, cancellationToken);

                if (taskItem is null)
                {var response = new Response(
                        null,
                        null,
                        null,
                        null,
                        null,
                        null
                    );
                    return Results.Ok(response);
                }

                else
                {
                    var response = new Response(
                        taskItem.Id,
                        taskItem.Title,
                        taskItem.Description,
                        taskItem.Status.ToString(),
                        taskItem.Projects,
                        taskItem.CreatedAt
                    );
                    return Results.Ok(response);
                }
            })
            .WithName("GetTaskItemById");
    }
}

