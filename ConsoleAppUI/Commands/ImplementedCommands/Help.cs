using ConsoleAppUI.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConsoleAppUI.Commands.ImplementedCommands
{
    public class Help : ICommands
    {
        public string commandName { get; set; } = "help";
        public string commandDescription { get; set; } = "Displays all available ICommands";
        public commandsEnums.Commands type { get; set; } = commandsEnums.Commands.global;

        public void Execute()
        {
            Console.WriteLine("Available commands:");
            AllCommands.commands.ToList().ForEach(x => Console.WriteLine($"{x.Key} - {x.Value.commandDescription}"));
        }
    }
}
