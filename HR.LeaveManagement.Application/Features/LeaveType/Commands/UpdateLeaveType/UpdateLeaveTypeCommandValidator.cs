using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        
        public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(70)
                .WithMessage("{PropertyName} must be fewer than 70 characters");

            RuleFor(p => p.DefaultDays)
                .LessThan(100)
                .WithMessage("{PropertyName} cannot exceed 100")
                .GreaterThan(1)
                .WithMessage("{PropertyName} cannot be less than 1");


            _leaveTypeRepository = leaveTypeRepository;
        }

        private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
        {
            return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }
    }
}
