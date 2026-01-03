using TaskManagement.Application.Wrapper;
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

    public WorkFlow WorkFlow { get; set; } = WorkFlow.TODO;

    public DateTime? DueDate { get; set; }

    public required Priority Priority { get; set; }

    #endregion Properties

    #region Methods

    public Result ChangeWorkFlow(WorkFlow newWorkFlow)
    {
        if (!WorkFlowStateMachine.CanTransition(WorkFlow, newWorkFlow))
            return Result.Failure($"Invalid workflow transition: {WorkFlow} → {newWorkFlow}");

        WorkFlow = newWorkFlow;

        return Result.Success();
    }

    public Result ReAssign(int userId)
    {
        UserId = userId;
        return Result.Success();
    }

    #endregion Methods
}
