using TaskManagement.Domain.Enum;
using TaskManagement.Application.Wrapper;
using TaskManagement.Application.Services.Dtos.TaskItemDto;
using TaskManagement.Application.Services.Dtos.TaskItemDto.Request;
using TaskManagement.Application.Services.Dtos.TaskItemDto.Response;

namespace Application.Services.Contracts.Task;

public interface ITaskService
{
    Task<Result<IList<TaskItemResponse>>> GetAllAsync(TaskQueryableDto queryableDto);

    Task<Result<TaskItemResponse>> GetByIdAsync(Guid guid, CancellationToken cancellationToken);

    Task<Result<Guid>> AddAsync(TaskItemCreateRequest request, CancellationToken cancellationToken);

    Task<Result> Update(TaskItemUpdateRequest request, Guid guid, CancellationToken cancellationToken);

    Task<Result> Delete(Guid id, CancellationToken cancellationToken);

    Task<Result> ChangeWorkFlow(WorkFlow newWorkFlow, Guid guid, CancellationToken cancellationToken);

    Task<Result> ChangePriority(Priority newPriority, Guid guid, CancellationToken cancellationToken);
}