namespace Application.Services.Implementations.Task;

using Contracts;
using Application.Services.Contracts.Task;
using TaskManagement.Domain.Entities;
using TaskManagement.InfraStructure.Persistence.Repositories.Interfaces;
using Dtos;
using TaskManagement.Domain.Enum;
public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TaskService(ITaskRepository taskRepository, IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<List<TaskItem>> GetAllAsync()
    {
        return _taskRepository.GetAllAsync();
    }

    public Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return _taskRepository.GetByIdAsync(id);
    }

    public async Task<TaskItem> AddAsync(TaskCreateDto request)
    {
        if (request.DueDate <= DateTime.Now)
        {
            throw new ArgumentException("DueDate must be in the future");
        }
        
        TaskItem taskItem = new TaskItem()
        {
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            Priority = request.Priority,
            DueDate = request.DueDate,
        };
        
        _taskRepository.Create(taskItem);
        await _unitOfWork.SaveChangesAsync();

        return taskItem;
    }

    public async Task<TaskItem> Update(TaskUpdateDto request, Guid id)
    {
        if (request.DueDate <= DateTime.Now)
        {
            throw new ArgumentException("DueDate must be in the future");
        }
        TaskItem? taskItem = await _taskRepository.GetByIdAsync(id);
        if (taskItem is null)
        {
            throw new ArgumentException("item not found");
            //TODO HANDLE WITH RESULT PATTERN SOON
        }
        taskItem.Name = request.Name;
        taskItem.Description = request.Description;
        taskItem.DueDate = request.DueDate;
        
        _taskRepository.Update(taskItem);
         await _unitOfWork.SaveChangesAsync();
         return taskItem;
    }
    public void Delete(TaskItem entity)
    {
        _taskRepository.Delete(entity);
        _unitOfWork.SaveChanges();
    }

    public async void ChangeWorkFlow(WorkFlow newWorkFlow, Guid id)
    {
        TaskItem? taskItem = await _taskRepository.GetByIdAsync(id);
        if (taskItem is null)
        {
            throw new ArgumentException("item not found");
            
            // TODO HANDLE WITH RESULT PATTERN SOON
        }
        taskItem.WorkFlow = newWorkFlow;
        _taskRepository.Update(taskItem);
        await _unitOfWork.SaveChangesAsync();
    }

    public async void ChangePriority(Priority newPriority, Guid id)
    {
        TaskItem? taskItem = await _taskRepository.GetByIdAsync(id);
        if (taskItem is null)
        {
            throw new ArgumentException("item not found");
            // TODO HANDLE WITH RESULT PATTERN SOON
        }
        taskItem.Priority = newPriority;
        _taskRepository.Update(taskItem);
        await _unitOfWork.SaveChangesAsync();
    }
}