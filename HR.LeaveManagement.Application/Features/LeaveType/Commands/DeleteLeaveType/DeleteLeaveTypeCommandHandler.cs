using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILogger<DeleteLeaveTypeCommandHandler> _logger;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, ILogger<DeleteLeaveTypeCommandHandler> logger)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // retrieve domain entity object
            var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);
            
            // verify that record exists
            if (leaveTypeToDelete is null)
            {
                _logger.LogWarning("No record found in delete request for {0}", request.Id);
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }
            
            // remove from database
            await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

            // return unit value
            return Unit.Value;
        }
    }
}
