using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enum;

namespace TaskManagement.InfraStructure.Persistence.Configurations;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.HasQueryFilter(c => !c.IsDeleted);

        builder.Property(t => t.UserId)
            .IsRequired();
        
        builder.Property(t => t.Name)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(t => t.Description)
            .HasMaxLength(5000)
            .IsRequired(false);
        
        builder.Property(t => t.WorkFlow)
            .HasDefaultValue(WorkFlow.TODO)
            .HasSentinel(WorkFlow.TODO);
        
        builder.Property(t => t.Priority)
            .IsRequired()
            ;
        
        builder.Property(t => t.DueDate)
            .HasMaxLength(50)
            .IsRequired();
    }
}