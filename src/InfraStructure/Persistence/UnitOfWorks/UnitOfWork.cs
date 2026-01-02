using TaskManagement.InfraStructure.Persistence.Context;

namespace TaskManagement.InfraStructure.Persistence.UnitOfWorks;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);

    public void SaveChanges()
       => context.SaveChanges();
}