using FluentValidation;
using TaskManagement.Application.Services.Dtos.TaskItemDto.Request;
using TaskManagement.Domain.Enum;

namespace TaskManagement.Application.Services.Validations.Task;

public class TaskItemCreateValidator : AbstractValidator<TaskItemCreateRequest>
{
    public TaskItemCreateValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is required")
            .MaximumLength(50)
            .WithMessage("Name cannot exceed 50 characters");

        RuleFor(t => t.UserId)
            .NotEmpty()
            .NotNull()
            .WithMessage("UserId is required");

        RuleFor(t => t.Description)
             .MaximumLength(5000)
             .WithMessage("Description can not have more than 5000 lenght");

        RuleFor(t => t.DueDate)
            .GreaterThan(DateTime.Now)
            .WithMessage("DueDate cannot be in the past date")
            .NotEmpty()
            .NotNull()
            .WithMessage("DueDate is required");

        RuleFor(t => t.Priority)
            .Must(p => Enum.IsDefined(typeof(Priority), p))
            .WithMessage("Priority is invalid")
            .NotEmpty()
            .NotNull()
            .WithMessage("Priority is required");
    }
}