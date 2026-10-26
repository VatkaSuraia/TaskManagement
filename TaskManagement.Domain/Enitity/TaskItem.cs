namespace TaskManagement.Domain.Enitity;

public class TaskItem
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
    
    public Guid Id { get; init; }
    
    public string Title { get; init; }
    
    public string? Description { get; init; }
    
    public Status Status { get; init; }
    
    public Guid Projects { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    public static TaskItem CreateTaskItem(string title, string? description, Status? status, Guid projects)
    {
        if(string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title)); //тут по хорошему нужен кастомный свой ErrorResult но FlValidation не хочу тянуть
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