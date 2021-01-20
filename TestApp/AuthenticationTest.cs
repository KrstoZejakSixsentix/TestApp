using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ServerBytes.Plugins.Authentication.Abstractions;
using ServerBytes.Plugins.Authentication.Client;

namespace TestApp
{
    public class AuthenticationTest
    {
        private readonly IAuthenticationService _service;

        public AuthenticationTest(IAuthenticationService service)
        {
            _service = service;
        }

        public async Task Start()
        {
            try
            {
                await CreateUsernamePasswordAccount();
                await Logout();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private async Task CreateUsernamePasswordAccount()
        {
            var username = $"User-{Guid.NewGuid().ToString().Substring(0, 8)}";
            var result = await _service.AuthenticateWithUsernamePasswordAsync(
                new AuthenticateWithUsernamePasswordLoginRequest
                {
                    AppVersion = "AppVersion",
                    DisplayName = username,
                    SyncDisplayName = true,
                    Operation = AuthenticateOperation.LoginOrRegister,
                    Password = "pass",
                    Username = username
                });

            Debug.Assert(result.IsSuccessful);
            Debug.Assert(result.Message == null);
            Console.WriteLine($"UserId: {result.Data.User.Id}");
        }

        private async Task Logout()
        {
            var result = await _service.LogOutAsync();
            Debug.Assert(result.IsSuccessful);
            Console.WriteLine("User logged out");
        }
    }
}
