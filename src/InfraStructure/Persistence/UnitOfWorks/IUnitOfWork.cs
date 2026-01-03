namespace TaskManagement.InfraStructure.Persistence.UnitOfWorks;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    void SaveChanges();
}