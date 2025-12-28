namespace TaskManagement.InfraStructure;

using TaskManagement.InfraStructure.Persistence.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext context)
    {
        _appDbContext = context;
    }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _appDbContext.SaveChangesAsync(cancellationToken);
    }

    public void SaveChanges()
    {
        _appDbContext.SaveChanges();
    }
}