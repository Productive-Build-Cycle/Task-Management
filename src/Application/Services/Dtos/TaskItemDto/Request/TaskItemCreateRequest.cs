using TaskManagement.Domain.Enum;

namespace TaskManagement.Application.Services.Dtos.TaskItemDto.Request;

public record TaskItemCreateRequest(
    int UserId,
    string Name,
    string Description,
    DateTime DueDate,
    Priority Priority
    );