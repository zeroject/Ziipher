using ConsoleAppUI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppUI.Helper
{
    public static class AllCommands
    {
        public static Dictionary<string, ICommands> commands = new Dictionary<string, ICommands>();

        public static void populateCommands()
        {
            var commandTypes = typeof(ICommands).Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICommands)));
            foreach (var type in commandTypes)
            {
                var command = (ICommands)Activator.CreateInstance(type);
                commands.Add(command.commandName, command);
            }
        }
    }
}
