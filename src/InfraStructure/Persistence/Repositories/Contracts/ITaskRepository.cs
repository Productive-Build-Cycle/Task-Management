using TaskManagement.Domain.Entities;
using TaskManagement.InfraStructure.Persistence.Specifications.Contracts;

namespace TaskManagement.InfraStructure.Persistence.Repositories.Interfaces;

public interface ITaskRepository
{
    IQueryable<TaskItem> GetAll(ISpecification<TaskItem>? specification);

    Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task AddAsync(TaskItem entity, CancellationToken cancellationToken);

    bool Update(TaskItem entity);

    void Delete(TaskItem entity);
}