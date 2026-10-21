namespace TaskManagement.Domain.Enitity;

public record Projects
{
    private Projects(Guid id, string name, string? description, DateTime created)
    {
        Id = id;
        Name = name;
        Description = description;
        Created = created;
    }
    public Guid Id { get; init; }
    
    string Name { get; init; }
    
    string? Description { get; init; }
    
    DateTime Created { get; init; }

    public static Projects CreateProject(string name, string? description, DateTime? created)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        return new Projects(
            Guid.NewGuid(),
            name,
            description ?? string.Empty,
            created ?? DateTime.UtcNow);
    }
}