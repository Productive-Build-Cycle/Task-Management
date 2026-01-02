using FluentValidation;
using TaskManagement.Application.Services.Dtos.TaskItemDto.Request;

namespace TaskManagement.Application.Services.Validations.Task;

public class TaskItemUpdateValidator : AbstractValidator<TaskItemUpdateRequest>
{
    public TaskItemUpdateValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is required")
            .MaximumLength(50)
            .WithMessage("Name cannot exceed 50 characters");

        RuleFor(t => t.Description)
            .MaximumLength(5000)
            .WithMessage("Description can not have more than 5000 lenght");

        RuleFor(t => t.DueDate)
            .GreaterThan(DateTime.Now)
            .WithMessage("DueDate cannot be in the past date")
            .NotEmpty().WithMessage("DueDate is required")
            .NotNull().WithMessage("DueDate is required");
    }
}