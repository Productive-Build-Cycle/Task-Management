namespace Application.Services.Validations;

using Application.Services.Dtos.TaskItemDto;
using FluentValidation;
using TaskManagement.Domain.Enum;

public class TaskItemCreateValidate : AbstractValidator<TaskItemCreateDto>
{
    public TaskItemCreateValidate()
    {
        RuleFor(t => t.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters")
            ;
        
        RuleFor(t => t.UserId)
            .NotEmpty().WithMessage("UserId is required")
            .NotNull().WithMessage("UserId is required")
            ;

        RuleFor(t => t.Description)
            .NotEmpty().WithMessage("Description is required")
            .NotNull().WithMessage("Description is required")
            ;
        
        RuleFor(t => t.DueDate)
            .GreaterThan(DateTime.Now)
            .WithMessage("DueDate cannot be in the future")
            .NotEmpty().WithMessage("DueDate is required")
            .NotNull().WithMessage("DueDate is required")
            ;
        
        RuleFor(t => t.Priority)
            .Must(p => Enum.IsDefined(typeof(Priority), p))
            .WithMessage("Priority is invalid")
            .NotEmpty().WithMessage("Priority is required")
            .NotNull().WithMessage("Priority is required")
            ;
        
    }
}