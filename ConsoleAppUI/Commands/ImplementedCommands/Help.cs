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
        public string CommandName { get; set; } = "help";
        public string CommandDescription { get; set; } = "Displays all available ICommands";
        public commandsEnums.Commands Type { get; set; } = commandsEnums.Commands.global;

        public void Execute()
        {
            Console.WriteLine("Available commands:");
            AllCommands.commands.ToList().ForEach(x => Console.WriteLine($"{x.Key} - {x.Value.CommandDescription}"));
        }
    }
}
