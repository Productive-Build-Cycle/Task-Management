namespace TaskManagement.InfraStructure.Persistence.Repositories.Interfaces;

using Domain.Entities;

public interface ITaskRepository
{
    IQueryable<TaskItem> GetAll();
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task AddAsync(TaskItem entity);
    Task Update(TaskItem entity);
    Task Delete(TaskItem entity);
}