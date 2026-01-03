namespace TaskManagement.InfraStructure;

using TaskManagement.Domain.Enum;
public record InfraTaskQueryableDto(
    int? UserId ,
    string? Search ,
    WorkFlow? WorkFlow ,
    Priority? Priority ,
    int? PageNumber ,
    int? PageSize ,
    bool SortByDueDateDesc )
;