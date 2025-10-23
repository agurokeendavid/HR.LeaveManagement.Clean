using AutoMapper;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly IMapper _mapper;

        public LeaveTypeService(IClient client, IMapper mapper) : base(client)
        {
            _mapper = mapper;
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypesAsync()
        {
            var leaveTypes = await _client.LeaveTypesAllAsync();
            return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetailsAsync(int id)
        {
            var leaveType = await _client.LeaveTypesGETAsync(id);
            return _mapper.Map<LeaveTypeVM>(leaveType);
        }

        public async Task<Response<Guid>> CreateLeaveTypeAsync(LeaveTypeVM leaveType)
        {
            try
            {
                var createLeaveTypeCommand = _mapper.Map<CreateLeaveTypeCommand>(leaveType);
                await _client.LeaveTypesPOSTAsync(createLeaveTypeCommand);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                return ConvertApiExceptions<Guid>(exception);
            }
        }

        public async Task<Response<Guid>> UpdateLeaveTypeAsync(int id, LeaveTypeVM leaveType)
        {
            try
            {
                var updateLeaveTypeCommand = _mapper.Map<UpdateLeaveTypeCommand>(leaveType);
                await _client.LeaveTypesPUTAsync(id.ToString(), updateLeaveTypeCommand);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                return ConvertApiExceptions<Guid>(exception);
            }
        }

        public async Task<Response<Guid>> DeleteLeaveTypeAsync(int id)
        {
            try
            {
                await _client.LeaveTypesDELETEAsync(id);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                return ConvertApiExceptions<Guid>(exception);
            }
        }
    }
}
