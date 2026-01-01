using FluentValidation;
using TaskManagement.Application.Services.Dtos.TaskItemDto.Request;
using TaskManagement.Domain.Enum;

namespace TaskManagement.Application.Services.Validations.Task;

public class ChangePriorityValidator : AbstractValidator<ChangePriorityRequest>
{
    public ChangePriorityValidator()
    {
        RuleFor(x => x.newPriority)
                .Must(p => Enum.IsDefined(typeof(Priority), p))
                .WithMessage("Invalid Priority value")
                .NotEmpty()
                .NotNull()
                .WithMessage("Priority is required"); ;
    }
}
