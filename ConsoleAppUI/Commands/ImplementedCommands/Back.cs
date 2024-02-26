using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppUI.Commands.ImplementedCommands
{
    public class Back : ICommands
    {
        public string commandName { get; set; } = "back";
        public string commandDescription { get; set; } = "Go back to MainMenu";
        public commandsEnums.Commands type { get; set; } = commandsEnums.Commands.global;

        public void Execute()
        {
            Console.WriteLine("Going back to MainMenu");
            Program.type = commandsEnums.Commands.mainMenu;
        }
    }
}
