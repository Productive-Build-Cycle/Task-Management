namespace TaskManagement.Application.Services.Dtos.TaskItemDto.Request;


public record TaskItemUpdateRequest(
    string Name,
    string Description,
    DateTime DueDate
    );