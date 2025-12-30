namespace Application.Services.Contracts.Task;

using TaskManagement.Domain.Enum;
using TaskManagement.Application.Wrapper;
using Dtos.TaskItemDto;

public interface ITaskService
{
    Task<Result<List<TaskItemResponseDto>>> GetAllAsync(); 
    Task<Result<TaskItemResponseDto>> GetByIdAsync(Guid guid); 
    Task<Result<Guid>> AddAsync(TaskItemCreateDto request); 
    Task<Result<TaskItemResponseDto>> Update(TaskItemUpdateDto request, Guid guid); 
    Task<Result> Delete(Guid id); 
    Task<Result> ChangeWorkFlow(WorkFlow newWorkFlow, Guid guid); 
    Task<Result> ChangePriority(Priority newPriority, Guid guid);
}