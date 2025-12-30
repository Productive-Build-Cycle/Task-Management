namespace Application.Services.Dtos;

using TaskManagement.Domain.Enum;

public class TaskQueryableDto
{
    public int? UserId { get; set; }
    public string? Search { get; set; }
    public WorkFlow? WorkFLow { get; set; }
    public Priority? Priority { get; set; }
    public string? OrderBy { get; set; }
    public bool? Descending { get; set; }
}