using TaskManagement.Domain.Enum;

namespace TaskManagement.Application.Services.Dtos.TaskItemDto.Request;

public record ChangePriorityRequest(Priority newPriority);
