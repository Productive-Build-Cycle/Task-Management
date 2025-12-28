namespace TaskManagement.InfraStructure;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    void SaveChanges();
}