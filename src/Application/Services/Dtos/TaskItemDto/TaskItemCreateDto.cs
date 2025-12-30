namespace Application.Services.Dtos.TaskItemDto;

using TaskManagement.Domain.Enum;

public record TaskItemCreateDto(
    string Name,
    int UserId,
    string Description,
    DateTime DueDate,
    Priority Priority
    );