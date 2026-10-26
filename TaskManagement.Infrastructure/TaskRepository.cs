using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Features;
using TaskManagement.Domain.Enitity;

namespace TaskManagement.Infrastructure;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _db;

    public TaskRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _db.TaskItem.FindAsync(id, cancellationToken);
    

    public async Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _db.TaskItem.ToListAsync(cancellationToken);
    

    public async Task AddAsync(TaskItem taskItem, CancellationToken cancellationToken = default) =>
        await _db.TaskItem.AddAsync(taskItem, cancellationToken);
    

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => 
        await _db.SaveChangesAsync(cancellationToken);

}