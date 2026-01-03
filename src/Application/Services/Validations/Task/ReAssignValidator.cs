using FluentValidation;
using TaskManagement.Application.Services.Dtos.TaskItemDto.Request;

namespace TaskManagement.Application.Services.Validations.Task;

public class ReAssignValidator : AbstractValidator<ReAssignRequest>
{
    public ReAssignValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .NotNull()
            .WithMessage("user id is required");
    }
}