using AuthenticationService.Dto;
using ConsoleAppUI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppUI.Commands.ImplementedCommands
{
    public class Login : ICommands
    {
        public string CommandName { get; set; } = "login";
        public string CommandDescription { get; set; } = "Login to the system";
        public commandsEnums.Commands type { get; set; } = commandsEnums.Commands.mainMenu;

        public void Execute()
        {
            string username;
            string password;
            string token;
            Console.WriteLine("Enter your username");
            username = Console.ReadLine();
            Console.WriteLine("Enter your password");
            password = Extra.ReadPassword();
            // Call the login method from the service layer
            Task<HttpResponseMessage> loginResponse = EndPointCaller.CallPostEndpointAsync(EndPoints.endpoints["Auth"] + "/loginuser", new LoginDto { Password = password, Username = username });
            while (!loginResponse.IsCompleted)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            token = loginResponse.Result.ToString();
            Program.token = token;
            Program.type = commandsEnums.Commands.LoginMenu;
            Console.WriteLine(token);
        }
    }
}
