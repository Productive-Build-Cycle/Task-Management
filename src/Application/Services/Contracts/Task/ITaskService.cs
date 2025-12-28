namespace Application.Services.Contracts.Task;

using TaskManagement.Domain.Entities;
using Application.Services.Dtos;
using TaskManagement.Domain.Enum;
public interface ITaskService
{
    Task<List<TaskItem>> GetAllAsync(); //TODO HANDLE WITH RESULT PATTERN SOON
    Task<TaskItem?> GetByIdAsync(Guid guid); //TODO HANDLE WITH RESULT PATTERN SOON
    Task<TaskItem> AddAsync(TaskCreateDto request); //TODO HANDLE WITH RESULT PATTERN SOON
    Task<TaskItem> Update(TaskUpdateDto request, Guid guid); //TODO HANDLE WITH RESULT PATTERN SOON
    void Delete(TaskItem entity); //TODO HANDLE WITH RESULT PATTERN SOON
    void ChangeWorkFlow(WorkFlow newWorkFlow, Guid guid); //TODO HANDLE WITH RESULT PATTERN SOON
    void ChangePriority(Priority newPriority, Guid guid); //TODO HANDLE WITH RESULT PATTERN SOON
}