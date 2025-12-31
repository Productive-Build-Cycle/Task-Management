namespace Application.Services.Implementations.Task;

using TaskManagement.InfraStructure;
using Application.Services.Contracts.Task;
using TaskManagement.Domain.Entities;
using TaskManagement.InfraStructure.Persistence.Repositories.Interfaces;
using TaskManagement.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Wrapper;
using Dtos.TaskItemDto;
using MapsterMapper;
using FluentValidation;
using Application.Services.Dtos;
using TaskManagement.Domain;
public class TaskService(
    ITaskRepository taskRepository, 
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IValidator<TaskItemCreateDto> createValidator,
    IValidator<TaskItemUpdateDto> updateValidator
    
    ) : ITaskService
{
    public async Task<Result<List<TaskItemResponseDto>>> GetAllAsync()
    {
        var q = taskRepository.GetAll();
       
        var res = await q.ToListAsync();

        var data = mapper.Map<List<TaskItemResponseDto>>(res);
        return Result<List<TaskItemResponseDto>>.Success(data);
    }

    public async Task<Result<TaskItemResponseDto>> GetByIdAsync(Guid id) 
    {
        var task = await taskRepository.GetByIdAsync(id);

        if (task is null)
        {
            return Result<TaskItemResponseDto>.Failure("Task not found");
        }
        
        var data = mapper.Map<TaskItemResponseDto>(task);
        
        return Result<TaskItemResponseDto>.Success(data);
    }

    public async Task<Result<Guid>> AddAsync(TaskItemCreateDto request)
    {
        var validationResult = await createValidator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            var err = validationResult.Errors.First().ErrorMessage;
            return Result<Guid>.Failure(
                err
            );
        }
        
        TaskItem taskItem = new TaskItem()
        {
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            Priority = request.Priority,
            DueDate = request.DueDate,
        };
        
        await taskRepository.AddAsync(taskItem);
        await unitOfWork.SaveChangesAsync();

        return Result<Guid>.Success(taskItem.GuidRow);
    }

    public async Task<Result<TaskItemResponseDto>> Update(TaskItemUpdateDto request, Guid id) 
    {
        
        var validationResult = await updateValidator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            var err = validationResult.Errors.First().ErrorMessage;
            
            return Result<TaskItemResponseDto>.Failure(
                err
            );
        }
        
        TaskItem? taskItem = await taskRepository.GetByIdAsync(id);
        if (taskItem is null)
        {
            return Result<TaskItemResponseDto>.Failure("Task not found");
        }
        taskItem.Name = request.Name;
        taskItem.Description = request.Description;
        taskItem.DueDate = request.DueDate;
        
         await taskRepository.Update(taskItem);
         await unitOfWork.SaveChangesAsync();
         
        var data = mapper.Map<TaskItemResponseDto>(taskItem);
         
         return Result<TaskItemResponseDto>.Success(data);
    }
    public async Task<Result> Delete(Guid id)
    { 
        var entity = await taskRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result.Failure("Task not found");
        }
        
        await taskRepository.Delete(entity);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> ChangeWorkFlow(WorkFlow newWorkFlow, Guid id)
    {
        TaskItem? taskItem = await taskRepository.GetByIdAsync(id);
        if (taskItem is null)
        {
            return Result.Failure("Task not found");
        }
        
        if (!Enum.IsDefined(typeof(WorkFlow), newWorkFlow))
        {
            return Result.Failure("Invalid workflow value");
        }
        
        if (!WorkFlowStateMachine.CanTransition(taskItem.WorkFlow, newWorkFlow))
            return Result.Failure(
                $"Invalid workflow transition: {taskItem.WorkFlow} → {newWorkFlow}");
        
        taskItem.WorkFlow = newWorkFlow;
        await taskRepository.Update(taskItem);
        await unitOfWork.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result> ChangePriority(Priority newPriority, Guid id) 
    {
        TaskItem? taskItem = await taskRepository.GetByIdAsync(id);
        if (taskItem is null)
        {
            return Result.Failure("Task not found");
        }
        
        if (!Enum.IsDefined(typeof(Priority), newPriority))
        {
            return Result.Failure("Invalid Priority value");
        }
        taskItem.Priority = newPriority;
        await taskRepository.Update(taskItem);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}