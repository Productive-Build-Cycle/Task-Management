namespace Application.Services.Contracts.Task;

using TaskManagement.Domain.Entities;
using Application.Services.Dtos;
using TaskManagement.Domain.Enum;
using TaskManagement.Application.Wrapper;
using Task = System.Threading.Tasks.Task;

public interface ITaskService
{
    Task<Result<List<TaskItem>>> GetAllAsync(); 
    Task<Result<TaskItem>> GetByIdAsync(Guid guid); 
    Task<Result<TaskItem>> AddAsync(TaskCreateDto request); 
    Task<Result<TaskItem>> Update(TaskUpdateDto request, Guid guid); 
    Task<Result> Delete(Guid id); 
    Task<Result> ChangeWorkFlow(WorkFlow newWorkFlow, Guid guid); 
    Task<Result> ChangePriority(Priority newPriority, Guid guid);
}