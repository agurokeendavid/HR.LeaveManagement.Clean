using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class Create : ComponentBase
{
    [Inject] ILeaveTypeService leaveTypeService { get; set; }
    [Inject] ILeaveRequestService leaveRequestService { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    LeaveRequestVM LeaveRequest { get; set; } = new LeaveRequestVM();
    List<LeaveTypeVM> leaveTypeVMs { get; set; } = new List<LeaveTypeVM>();

    protected override async Task OnInitializedAsync()
    {
        leaveTypeVMs = await leaveTypeService.GetLeaveTypesAsync();
    }

    private async Task HandleValidSubmit()
    {
        // Perform form submission here
        await leaveRequestService.CreateLeaveRequestAsync(LeaveRequest);
        NavigationManager.NavigateTo("/leaverequests/");
    }
}