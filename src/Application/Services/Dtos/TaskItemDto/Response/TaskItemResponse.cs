namespace TaskManagement.Application.Services.Dtos.TaskItemDto.Response;

using TaskManagement.Domain.Enum;
public record TaskItemResponse(
    Guid GuidRow,
    int UserId,
    string Name,
    string Description,
    DateTime DueDate,
    Priority Priority,
    WorkFlow WorkFlow
);