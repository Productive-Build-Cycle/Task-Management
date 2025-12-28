namespace Application.Services.Dtos;

using TaskManagement.Domain.Enum;

public class TaskDto
{
    
}

public class TaskCreateDto
{
    public required string Name { get; set; }
    public int UserId { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
}

public class TaskUpdateDto
{
    public required string Name { get; set; }
    public int UserId { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
}

public class TaskResponseDto
{
    public required Guid GuidRow { get; set; }
    public required string Name { get; set; }
    public int UserId { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class TaskQueryableDto
{
    public int? UserId { get; set; }
    public string? Search { get; set; }
    public WorkFlow? WorkFLow { get; set; }
    public Priority? Priority { get; set; }
    public string? OrderBy { get; set; }
    public bool? Descending { get; set; }
}