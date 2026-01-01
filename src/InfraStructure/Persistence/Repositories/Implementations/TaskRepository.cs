namespace TaskManagement.InfraStructure.Persistence.Repositories.Implementations;

using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Context;
using Interfaces;
using Specifications.Contracts;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _appDbContext;

    public TaskRepository(AppDbContext context)
    {
        _appDbContext = context;
    }
    public IQueryable<TaskItem> GetAll(ISpecification<TaskItem>? specification)
    {

        if (specification is null)
        {
            return _appDbContext.TaskItems;
        }
        
        return SpecificationEvaluator<TaskItem>
            .GetQuery(
                _appDbContext.TaskItems.AsQueryable(), specification);
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
       return await _appDbContext.TaskItems.FirstOrDefaultAsync(t => t.GuidRow == id);
    }

    public async Task AddAsync(TaskItem entity)
    {
       await _appDbContext.TaskItems.AddAsync(entity);
    }

    public async Task Update(TaskItem entity)
    {
        entity.MarkAsUpdated();
        _appDbContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task Delete(TaskItem entity)
    {
        entity.MarkAsDeleted();
    }
}