using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeVM>> GetLeaveTypesAsync();
        Task<LeaveTypeVM> GetLeaveTypeDetailsAsync(int id);
        Task<Response<Guid>> CreateLeaveTypeAsync(LeaveTypeVM leaveType);
        Task<Response<Guid>> UpdateLeaveTypeAsync(int id, LeaveTypeVM leaveType);
        Task<Response<Guid>> DeleteLeaveTypeAsync(int id);
    }
}
