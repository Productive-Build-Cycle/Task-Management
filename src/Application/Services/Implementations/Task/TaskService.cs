using Application.Services.Contracts.Task;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Services.Dtos.TaskItemDto;
using TaskManagement.Application.Services.Dtos.TaskItemDto.Request;
using TaskManagement.Application.Services.Dtos.TaskItemDto.Response;
using TaskManagement.Application.Wrapper;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enum;
using TaskManagement.InfraStructure;
using TaskManagement.InfraStructure.Persistence.Repositories.Interfaces;
using TaskManagement.InfraStructure.Persistence.Specifications;
using TaskManagement.InfraStructure.Persistence.UnitOfWorks;
using TaskManagement.InfraStructure.Services.Contracts;

namespace Application.Services.Implementations.Task;

public class TaskService(
    ITaskRepository taskRepository,
    ICacheService cacheService,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : ITaskService
{
    #region Get All

    public async Task<Result<IList<TaskItemResponse>>> GetAllAsync(TaskQueryableDto queryableDto)
    {
        var infraTaskQueryableDto = mapper.Map<InfraTaskQueryableDto>(queryableDto);

        var taskItemSpecification = new TaskItemSpecification(infraTaskQueryableDto);

        var query = taskRepository.GetAll(taskItemSpecification);

        var data = mapper.Map<List<TaskItemResponse>>(await query.ToListAsync());

        return Result<IList<TaskItemResponse>>.Success(data);
    }

    #endregion Get All

    #region Get By Id

    public async Task<Result<TaskItemResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(id, cancellationToken);

        if (task is null)
            return Result<TaskItemResponse>.Failure(CommonMessages.NotFoundMessage);

        return Result<TaskItemResponse>.Success(mapper.Map<TaskItemResponse>(task));
    }

    #endregion Get By Id

    #region Add

    public async Task<Result<Guid>> AddAsync(TaskItemCreateRequest request, CancellationToken cancellationToken)
    {
        var user = await cacheService.GetAsync<Dictionary<string, string>>(request.UserId.ToString());

        if (user is null)
            throw new Exception("User Not Found");

        var taskItem = mapper.Map<TaskItem>(request);

        await taskRepository.AddAsync(taskItem, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(taskItem.GuidRow);
    }

    #endregion Add

    #region Update

    public async Task<Result> Update(TaskItemUpdateRequest request, Guid id, CancellationToken cancellationToken)
    {
        TaskItem? taskItem = await taskRepository.GetByIdAsync(id, cancellationToken);

        if (taskItem is null)
            return Result.Failure(CommonMessages.NotFoundMessage);

        mapper.Map(request, taskItem);

        taskRepository.Update(taskItem);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    #endregion Update

    #region Delete

    public async Task<Result> Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await taskRepository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
            return Result.Failure(CommonMessages.NotFoundMessage);

        taskRepository.Delete(entity);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    #endregion Delete

    #region Change Work Flow

    public async Task<Result> ChangeWorkFlow(WorkFlow newWorkFlow, Guid id, CancellationToken cancellationToken)
    {
        var taskItem = await taskRepository.GetByIdAsync(id, cancellationToken);

        if (taskItem is null)
            return Result.Failure(CommonMessages.NotFoundMessage);

        var result = taskItem.ChangeWorkFlow(newWorkFlow);

        if (!result.IsSuccess)
            return result;

        taskRepository.Update(taskItem);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    #endregion Change Work Flow

    #region Change Priority

    public async Task<Result> ChangePriority(Priority newPriority, Guid id, CancellationToken cancellationToken)
    {
        TaskItem? taskItem = await taskRepository.GetByIdAsync(id, cancellationToken);

        if (taskItem is null)
            return Result.Failure(CommonMessages.NotFoundMessage);

        taskItem.Priority = newPriority;

        taskRepository.Update(taskItem);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    #endregion Change Priority
}