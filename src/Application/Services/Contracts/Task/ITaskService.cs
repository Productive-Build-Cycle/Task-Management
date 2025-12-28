using TaskManagement.Domain.Enum;

namespace Application.Services.Contracts.Task;

using TaskManagement.Domain.Entities;
using Application.Services.Dtos;
public interface ITaskService
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(Guid guid);
    Task<TaskItem> AddAsync(TaskCreateDto request);
    Task<TaskItem> Update(TaskUpdateDto request, Guid guid);
    void Delete(TaskItem entity);
    void ChangeWorkFlow(WorkFlow newWorkFlow, Guid guid);
    void ChangePriority(Priority newPriority, Guid guid);
}