using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppUI.Commands
{
    public interface ICommands
    {
        /// <summary>
        /// string to listen after in commandLine
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// string description of command for help
        /// </summary>
        public string CommandDescription { get; set; }

        /// <summary>
        /// Type of command to control when user can execute ICommands
        /// </summary>
        public commandsEnums.Commands Type { get; set; }

        /// <summary>
        /// Needs to be overridden by Implementing class
        /// </summary>
        public void Execute();
    }
}
