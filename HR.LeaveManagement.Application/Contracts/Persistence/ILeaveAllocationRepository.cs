using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationDetailsAsync(int id);
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync();
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync(string userId);
    Task<bool> AllocationExistsAsync(string userId, int leaveTypeId, int period);
    Task AddAllocationsAsync(List<LeaveAllocation> allocations);
    Task<LeaveAllocation> GetUserAllocationsAsync(string userId, int leaveTypeId);
}