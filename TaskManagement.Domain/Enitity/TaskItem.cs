namespace TaskManagement.Domain.Enitity;

public record TaskItem
{
    private TaskItem(Guid id, string title, string? description, Status status, Guid projects, DateTime createdAt)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = status;
        Projects = projects;
        CreatedAt = createdAt;
    }
    
    Guid Id { get; init; }
    
    string Title { get; init; }
    
    string? Description { get; init; }
    
    Status? Status { get; init; }
    
    Guid Projects { get; init; }
    
    DateTime CreatedAt { get; init; }
    
    public static TaskItem CreateTaskItem(string title, string? description, Status? status, Guid projects)
    {
        if(string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title));
        if(projects == Guid.Empty)
            throw new ArgumentException("Нельзя создать задачу для несуществующего проекта");

        return new TaskItem(
            Guid.NewGuid(),
            title,
            description,
            status ?? Enitity.Status.Todo,
            projects,
            createdAt: DateTime.Now);
    }
}

public enum Status
{
    Todo,
    InProgress,
    Done
}