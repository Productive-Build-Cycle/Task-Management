using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskManagement.Domain.Entities;
using TaskManagement.InfraStructure.Persistence.Context;
using TaskManagement.InfraStructure.Persistence.Repositories.Interfaces;
using TaskManagement.InfraStructure.Persistence.Specifications.Contracts;

namespace TaskManagement.InfraStructure.Persistence.Repositories.Implementations;

public class TaskRepository(AppDbContext context) : ITaskRepository
{
    #region Get All

    public IQueryable<TaskItem> GetAll(ISpecification<TaskItem>? specification)
    {
        if (specification is null)
            return context.TaskItems
                .AsNoTracking()
                .AsQueryable();

        return SpecificationEvaluator<TaskItem>.GetQuery(context.TaskItems.AsQueryable(), specification);
    }

    #endregion Get All

    #region Get by Id

    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
           => await context.TaskItems.SingleOrDefaultAsync(x => x.GuidRow.Equals(id), cancellationToken);

    #endregion Get by Id

    #region Add

    public async Task AddAsync(TaskItem entity, CancellationToken cancellationToken)
         => await context.TaskItems.AddAsync(entity, cancellationToken);

    #endregion Add

    #region Update

    public bool Update(TaskItem entity)
    {
        EntityEntry result = context.Update(entity);

        entity.MarkAsUpdated();

        return result.State == EntityState.Modified;
    }

    #endregion Update

    #region Delete

    public void Delete(TaskItem entity)
        => entity.MarkAsDeleted();

    #endregion Delete
}