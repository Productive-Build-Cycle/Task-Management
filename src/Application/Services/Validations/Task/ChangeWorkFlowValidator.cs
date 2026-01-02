using FluentValidation;
using TaskManagement.Application.Services.Dtos.TaskItemDto.Request;
using TaskManagement.Domain.Enum;

namespace TaskManagement.Application.Services.Validations.Task;

public class ChangeWorkFlowValidator : AbstractValidator<ChangeWorkFlowRequest>
{
    public ChangeWorkFlowValidator()
    {
        RuleFor(x => x.workFlow)
            .Must(p => Enum.IsDefined(typeof(WorkFlow), p))
            .WithMessage("Invalid workflow value")
            .NotEmpty()
            .NotNull()
            .WithMessage("workflow is required");
    }
}
