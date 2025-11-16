using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Index
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILeaveTypeService LeaveTypeService { get; set; }
        
        [Inject]
        public ILeaveAllocationService LeaveAllocationService { get; set; }

        public List<LeaveTypeVM> LeaveTypes { get; set; }

        public string Message { get; set; } = string.Empty;
        
        protected void CreateLeaveType()
        {
            NavigationManager.NavigateTo("/leavetypes/create/");
        }

        protected void AllocateLeaveType(int id)
        {
            LeaveAllocationService.CreateLeaveAllocationAsync(id);
        }

        protected void EditLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
        }

        protected void DetailsLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/details/{id}");
        }

        protected async Task DeleteLeaveType(int id)
        {
            var response = await LeaveTypeService.DeleteLeaveTypeAsync(id);

            if (response.Success)
            {
                StateHasChanged();
                return;
            }

            Message = response.Message;
        }

        protected override async Task OnInitializedAsync()
        {
            LeaveTypes = await LeaveTypeService.GetLeaveTypesAsync();
        }
    }
}
