using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public AuthenticationService(IClient client,
            ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationStateProvider) : base(client, localStorageService)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            try
            {
                var authRequest = new AuthRequest()
                {
                    Email = email,
                    Password = password
                };

                var authResponse = await _client.LoginAsync(authRequest);

                if (string.IsNullOrWhiteSpace(authResponse.Token))
                    return false;

                await _localStorageService.SetItemAsync("token", authResponse.Token);
                // set claims in Blazor and login state
                await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();
                
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email, string password)
        {
            var regRequest = new RegistrationRequest()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = email,
                Password = password
            };

            var response = await _client.RegisterAsync(regRequest);

            if (!string.IsNullOrWhiteSpace(response.UserId))
            {
                return true;
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            // remove claims in Blazor and invalidate login state
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }
    }
}
