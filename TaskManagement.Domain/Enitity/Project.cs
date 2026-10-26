namespace TaskManagement.Domain.Enitity;

public class Project
{
    private Project(Guid id, string name, string? description, DateTime created)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = created;
    }
    public Guid Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public DateTime CreatedAt { get; init; }

    public static Project CreateProject(string name, string? description, DateTime? created)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        return new Project(
            Guid.NewGuid(),
            name,
            description ?? string.Empty,
            created ?? DateTime.UtcNow);
    }
}