using TaskManagement.Domain.Common;
using TaskManagement.Domain.Enum;

#nullable enable

namespace TaskManagement.Domain.Entities;

public class TaskItem : BaseEntity<long>
{
    #region Properties

    public required int UserId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public required WorkFlow WorkFlow { get; set; }

    public DateTime? DueDate { get; set; }

    public required Priority Priority { get; set; }

    #endregion Properties
}
