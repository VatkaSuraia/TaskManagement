using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using TaskManagement.Domain.Enitity;

namespace TaskManagement.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> TaskItem => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Название схемы но по дефолту и/или если конфа одна то можно не писать
        modelBuilder.HasDefaultSchema("education");

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Project", "education");
            entity.HasKey(k => k.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasMaxLength(500);
            
        });
        
        
        //Принимает настройки конфигрурации из места где вообще все сконфигурированно
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}