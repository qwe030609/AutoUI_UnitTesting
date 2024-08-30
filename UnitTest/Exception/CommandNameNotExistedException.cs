using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    public class CommandNameNotExistedException : Exception
    {
        // Custom property to hold the command name
        public string CommandName { get; }

        // Constructor that accepts the command name as a parameter
        public CommandNameNotExistedException(string commandName)
            : base($"The command \"{commandName}\" does not exist.")
        {
            CommandName = commandName;
        }

        // Optional: You can add additional constructors to handle inner exceptions or custom messages
        public CommandNameNotExistedException(string commandName, string message)
            : base(message)
        {
            CommandName = commandName;
        }

        public CommandNameNotExistedException(string commandName, string message, Exception innerException)
            : base(message, innerException)
        {
            CommandName = commandName;
        }
    }
}
