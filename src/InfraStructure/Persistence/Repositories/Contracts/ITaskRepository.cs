namespace TaskManagement.InfraStructure.Persistence.Repositories.Interfaces;

using Domain.Entities;

public interface ITaskRepository
{
    IQueryable<TaskItem> GetAll();
    Task<TaskItem?> GetByIdAsync(Guid id);
    void AddAsync(TaskItem entity);
    void Update(TaskItem entity);
    void Delete(TaskItem entity);
}