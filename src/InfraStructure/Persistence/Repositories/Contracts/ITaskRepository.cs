namespace TaskManagement.InfraStructure.Persistence.Repositories.Interfaces;

using Domain.Entities;
using TaskManagement.InfraStructure.Specifications.Contracts;
public interface ITaskRepository
{
    IQueryable<TaskItem> GetAll(ISpecification<TaskItem>? specification);
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task AddAsync(TaskItem entity);
    Task Update(TaskItem entity);
    Task Delete(TaskItem entity);
}