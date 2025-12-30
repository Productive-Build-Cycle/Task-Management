namespace Application.Services.Implementations.Task;

using TaskManagement.InfraStructure;
using Application.Services.Contracts.Task;
using TaskManagement.Domain.Entities;
using TaskManagement.InfraStructure.Persistence.Repositories.Interfaces;
using Dtos;
using TaskManagement.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Wrapper;

public class TaskService(ITaskRepository taskRepository, IUnitOfWork unitOfWork) : ITaskService
{
    public async Task<Result<List<TaskItem>>> GetAllAsync()
    {
        var q = taskRepository.GetAll();

        var res = await q.ToListAsync();

        return Result<List<TaskItem>>.Success(res);
    }

    public async Task<Result<TaskItem>> GetByIdAsync(Guid id) 
    {
        var task = await taskRepository.GetByIdAsync(id);

        if (task is null)
        {
            return Result<TaskItem>.Failure("Task not found");
        }
        
        return Result<TaskItem>.Success(task);
    }

    public async Task<Result<Guid>> AddAsync(TaskCreateDto request)
    {
        if (request.DueDate <= DateTime.Now)
        {
            return Result<Guid>.Failure("Due date cannot be in the future");
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

    public async Task<Result<TaskItem>> Update(TaskUpdateDto request, Guid id) 
    {
        if (request.DueDate <= DateTime.Now)
        {
            return Result<TaskItem>.Failure("Due date cannot be in the future");
        }
        TaskItem? taskItem = await taskRepository.GetByIdAsync(id);
        if (taskItem is null)
        {
            return Result<TaskItem>.Failure("Task not found");
        }
        taskItem.Name = request.Name;
        taskItem.Description = request.Description;
        taskItem.DueDate = request.DueDate;
        
        taskRepository.Update(taskItem);
         await unitOfWork.SaveChangesAsync();
         return Result<TaskItem>.Success(taskItem);
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
        taskItem.Priority = newPriority;
        taskRepository.Update(taskItem);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}