namespace TaskManagement.InfraStructure.Persistence.Repositories.Implementations;

using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Context;
using Interfaces;
public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _appDbContext;

    public TaskRepository(AppDbContext context)
    {
        _appDbContext = context;
    }
    public async Task<List<TaskItem>> GetAllAsync()
    {
        IQueryable<TaskItem> q = _appDbContext.TaskItems;
        
        // TODO APPY FILTERING AND ORDERING AND SEARCH
        
        return await q.ToListAsync();
    }

    public Task<TaskItem?> GetByIdAsync(Guid id)
    {
       return _appDbContext.TaskItems.FirstOrDefaultAsync(t => t.GuidRow == id);
    }

    public void Create(TaskItem entity)
    {
        _appDbContext.TaskItems.Add(entity);
    }

    public void Update(TaskItem entity)
    {
        entity.MarkAsUpdated();
        _appDbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TaskItem entity)
    {
        entity.MarkAsDeleted();
    }
}