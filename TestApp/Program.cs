using System;
using ServerBytes.Client;
using ServerBytes.Client.Abstractions;
using ServerBytes.Plugins.Authentication.Client;

namespace TestApp
{
    class Program
    {
        private static IClient Client;
        private const string appKey = "23d4cc5d-d2fa-4ebe-955e-bffe69e7ebaf";
        public static AuthenticationServiceFactory AuthFactory { get; set; }
        static void Main(string[] args)
        {
            var pluginHost = ClientFactory.GetPluginHost(appKey, "development");
            AuthFactory = new AuthenticationServiceFactory(pluginHost);


            Client = ClientFactory.GetClient(pluginHost);
            Client.OnConnected += Client_OnConnected;
            Client.OnFailedToConnect += Client_OnFailedToConnect;
            Client.OnDisconnected += Client_OnDisconnected;
            Console.WriteLine("Connecting");
            Client.Connect();

            Console.WriteLine("Press any key to stop the test");
            Console.Read();
            Console.WriteLine("Test stopped");
        }


        private static void Client_OnDisconnected(string errorMessage)
        {
            Console.WriteLine("Disconnected: {0}", errorMessage);
        }

        private static void Client_OnFailedToConnect(string errorMessage)
        {
            Console.WriteLine("Client_OnFailedToConnect: {0}", errorMessage);
        }

        private static void Client_OnConnected()
        {
            Console.WriteLine("Client_OnConnected");
            var authService = AuthFactory.Create(Client);

            var authTest = new AuthenticationTest(authService);
            authTest.Start()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
    }
}
