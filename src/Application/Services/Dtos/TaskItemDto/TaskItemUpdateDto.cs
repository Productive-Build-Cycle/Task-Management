namespace Application.Services.Dtos.TaskItemDto;


public record TaskItemUpdateDto(
    string Name,
    string Description,
    DateTime DueDate
    );