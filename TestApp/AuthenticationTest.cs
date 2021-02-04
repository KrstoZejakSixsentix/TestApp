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

        public async Task Start(string username)
        {
            try
            {
                //for (int i=1;i<10;i++)
                //{
               await CreateUsernamePasswordAccount(username);
                 //await CreateEmailPasswordAccount(i);
                //  await CreateEmailPasswordAccount();
                 await Logout();
                 // await CreateUsernamePasswordAccount(); izmeniti da bude za token
                 //await CreateUsernamePasswordAccount(); izmeniti da bude za async
                //await Logout();
               // }
                
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}"); //probati sa Stacktrace
            }
        }

        private async Task CreateUsernamePasswordAccount(string username) 
        {
            //var username = "J-123";
            var result = await _service.AuthenticateWithUsernamePasswordAsync(
                new AuthenticateWithUsernamePasswordLoginRequest
                {
                    AppVersion = "AppVersion",
                    //DisplayName = "User-123",
                    DisplayName = username,
                    SyncDisplayName = true,
                    Operation = AuthenticateOperation.LoginOrRegister,
                    Password = "pass",
                    //Username = "Test4321"
                    Username = username
                });

            Debug.Assert(result.IsSuccessful);
            Debug.Assert(result.Message == null);
            Console.WriteLine($"UserId: {result.Data.User.Id}");
            Console.WriteLine("username: " + username);

        }

        private async Task CreateEmailPasswordAccount()
        {
            //var email = "test" + i + "@gmail.com";
            var email = "testjgmail.com";
            var result = await _service.AuthenticateWithEmailPasswordAsync(
                new AuthenticateWithEmailPasswordLoginRequest
                {
                    AppVersion = "AppVersion",
                    DisplayName = email,
                    SyncDisplayName = true,
                    Operation = AuthenticateOperation.LoginOrRegister,
                    Password = "pass",
                    Email = email
                });

            //Debug.Assert(result.IsSuccessful);  
            Console.WriteLine(result.Message);  
            Debug.Assert(result.Message == null);
            Console.WriteLine($"UserId: {result.Data.User.Id}");
            Console.WriteLine("Email: " + email);
        }

        private async Task Logout()
        {
            var result = await _service.LogOutAsync();
            Debug.Assert(result.IsSuccessful);
            Console.WriteLine("User logged out");
        }
    }
}
