namespace TaskManagement.InfraStructure.Persistence.Repositories.Interfaces;

using Domain.Entities;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync(); // TODO WILL MODIFY TO AsQueryable
    Task<TaskItem?> GetByIdAsync(Guid id);
    void Create(TaskItem entity);
    void Update(TaskItem entity);
    void Delete(TaskItem entity);
}