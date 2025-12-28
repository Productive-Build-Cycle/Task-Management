using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;

namespace TaskManagement.InfraStructure.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> TaskItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<TaskItem>()
            .ToTable("TaskItems", "task");
        
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        foreach (var item in modelBuilder.Model.GetEntityTypes())
        {
            modelBuilder.Entity(item.ClrType).Property<bool>("IsDeleted").HasDefaultValue(false);
            modelBuilder.Entity(item.ClrType).Property<DateTime>("CreatedDate").HasMaxLength(50);
            modelBuilder.Entity(item.ClrType).Property<DateTime?>("UpdatedDate").HasDefaultValue(null)
                .HasMaxLength(50);
        }
    }
}