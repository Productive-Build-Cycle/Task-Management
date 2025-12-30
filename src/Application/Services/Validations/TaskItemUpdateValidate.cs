namespace Application.Services.Validations;

using Dtos.TaskItemDto;
using FluentValidation;

public class TaskItemUpdateValidate : AbstractValidator<TaskItemUpdateDto>
{
    public TaskItemUpdateValidate()
    {
        RuleFor(t => t.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters")
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
        
    }
}