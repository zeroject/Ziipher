using ConsoleAppUI.Helper;
using Domain.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppUI.Commands.ImplementedCommands
{
    public class AddUser : ICommands
    {
        public string CommandName { get; set; } = "adduser";
        public string CommandDescription { get; set; } = "Adds a user to the system";
        public commandsEnums.Commands Type { get; set; } = commandsEnums.Commands.mainMenu;

        public void Execute()
        {
            string username;
            string password;
            Console.WriteLine("Enter your username");
            username = Console.ReadLine();
            Console.WriteLine("Enter your password");
            password = Extra.ReadPassword();
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your age");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your email");
            string email = Console.ReadLine();
            // Call the add user method from the service layer
            Task<HttpResponseMessage> response = EndPointCaller.CallPostEndpointAsync(EndPoints.endpoints["User"] + $"/AddUser?username={username}&password={password}", new UserDTO { Id = 1, Name = name, Age = age, Email = email });
            while (!response.IsCompleted)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            if (response.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("\nUser added successfully");
            }
            else
            {
                Console.WriteLine("\nUser not added " + response.Result.ToString());
            }
        }
    }
}
