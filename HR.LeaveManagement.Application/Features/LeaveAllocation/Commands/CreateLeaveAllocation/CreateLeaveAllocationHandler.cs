using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IUserService _userService;

    public CreateLeaveAllocationHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IUserService userService)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _userService = userService;
    }
    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
        
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Leave Allocation Request", validationResult);
        }
        
        // get leave type for allocations
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);
        
        // get employees
        var employees = await _userService.GetEmployeesAsync();
        
        // get period
        var period = DateTime.Now.Year;
        
        // assign allocations if an allocation doesn't already exist for period and leave type
        var allocations = new List<Domain.LeaveAllocation>();
        foreach (var employee in employees)
        {
            var allocationExists = await _leaveAllocationRepository.AllocationExistsAsync(employee.Id, request.LeaveTypeId, period);
            if (allocationExists == false)
            {
                allocations.Add(new Domain.LeaveAllocation()
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveType.Id,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = period,
                });
            }
        }

        if (allocations.Any())
        {
            await _leaveAllocationRepository.AddAllocationsAsync(allocations);
        }
        
        return Unit.Value;
    }
}