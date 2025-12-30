namespace Application.Services.Dtos.TaskItemDto;

using TaskManagement.Domain.Enum;
public record TaskItemResponseDto(
    Guid GuidRow,
    string Name,
    string Description,
    DateTime DueDate,
    Priority Priority,
    WorkFlow WorkFlow
);