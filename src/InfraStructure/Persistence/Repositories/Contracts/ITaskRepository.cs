namespace TaskManagement.InfraStructure.Persistence.Repositories.Interfaces;

using Domain.Entities;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(Guid id);
    void Create(TaskItem entity);
    void Update(TaskItem entity);
    void Delete(TaskItem entity);
}