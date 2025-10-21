using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id);
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync();
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(string userId);
}