using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Features;
using TaskManagement.Domain.Enitity;

namespace TaskManagement.Infrastructure;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _db;

    public ProjectRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _db.Projects.FindAsync(id, cancellationToken);
    

    public async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _db.Projects.ToListAsync(cancellationToken);
    

    public async Task AddAsync(Project project, CancellationToken cancellationToken = default) =>
        await _db.Projects.AddAsync(project, cancellationToken);
    

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => 
        await _db.SaveChangesAsync(cancellationToken);
 
}