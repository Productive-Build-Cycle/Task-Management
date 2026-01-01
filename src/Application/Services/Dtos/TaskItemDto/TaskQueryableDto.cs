namespace TaskManagement.Application.Services.Dtos.TaskItemDto;

using TaskManagement.Domain.Enum;
public record TaskQueryableDto
(
    int? UserId ,
    string? Search ,
    WorkFlow? WorkFlow ,
    Priority? Priority ,
    string? OrderBy ,
    bool? Descending ,
    int? PageNumber ,
    int? PageSize ,
    bool SortByDueDateDesc )
    ;