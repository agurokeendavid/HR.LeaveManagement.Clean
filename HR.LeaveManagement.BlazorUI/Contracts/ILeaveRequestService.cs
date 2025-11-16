using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Contracts;

public interface ILeaveRequestService
{
    Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestListAsync();
    Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequestsAsync();
    Task<Response<Guid>> CreateLeaveRequestAsync(LeaveRequestVM leaveRequest);
    Task<LeaveRequestVM> GetLeaveRequestAsync(int id);
    Task<Response<Guid>> DeleteLeaveRequestAsync(int id);
    Task<Response<Guid>> ApproveLeaveRequestAsync(int id, bool approved);
    Task<Response<Guid>> CancelLeaveRequestAsync(int id);
}