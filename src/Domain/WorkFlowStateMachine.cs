using TaskManagement.Domain.Enum;

namespace TaskManagement.Domain;

public static class WorkFlowStateMachine
{
    private static readonly Dictionary<WorkFlow, WorkFlow[]> AllowedTransitions =
        new()
        {
            { WorkFlow.TODO,       new[] { WorkFlow.InProgress } },
            { WorkFlow.InProgress, new[] { WorkFlow.Done } },
            { WorkFlow.Done,       Array.Empty<WorkFlow>() }
        };

    public static bool CanTransition(WorkFlow from, WorkFlow to)
    {
        return AllowedTransitions.TryGetValue(from, out var allowed)
               && allowed.Contains(to);
    }
}