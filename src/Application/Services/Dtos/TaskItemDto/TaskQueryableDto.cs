using TaskManagement.Domain.Enum;

namespace TaskManagement.Application.Services.Dtos.TaskItemDto;

#nullable enable

public record TaskQueryableDto
(
    int? UserId,
    string? Search,
    WorkFlow? WorkFlow,
    Priority? Priority,
    int? PageNumber,
    int? PageSize,
    bool? SortByDueDateDesc,
    bool? SortByPriorityDesc,
    bool? SortByWorkFlowDesc
    );