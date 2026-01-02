namespace TaskManagement.InfraStructure.Persistence.Specifications;

using Domain.Entities;
using Contracts;
public sealed class TaskItemSpecification : BaseSpecification<TaskItem>
{
    public TaskItemSpecification(InfraTaskQueryableDto dto)
        : base(t =>
            (!dto.UserId.HasValue || t.UserId == dto.UserId) &&
            (!dto.Priority.HasValue || t.Priority == dto.Priority) &&
            (!dto.Priority.HasValue || t.Priority == dto.Priority) &&
            (!dto.WorkFlow.HasValue || t.WorkFlow == dto.WorkFlow) &&
            (string.IsNullOrWhiteSpace(dto.Search) ||
             t.Name.ToLower().Contains(dto.Search.ToLower())))
    {
        if (dto.SortByDueDateDesc)
            ApplyOrderByDescending(t => t.DueDate);

        else
            ApplyOrderBy(t => t.DueDate);

        if (dto.PageNumber.HasValue && dto.PageSize.HasValue)
            ApplyPaging((dto.PageNumber.Value - 1) * dto.PageSize.Value, dto.PageSize.Value);
    }
}