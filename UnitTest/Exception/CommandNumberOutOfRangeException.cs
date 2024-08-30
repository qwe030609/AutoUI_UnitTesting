using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    public class CommandNumberOutOfRangeException : Exception
    {
        // Custom property to hold the command name
        public int CommandNumber { get; }

        // Constructor that accepts the command number as a parameter
        public CommandNumberOutOfRangeException(int commandNumber)
            : base($"The CommandNumber \"{commandNumber}\" is Out Of Range.")
        {
            CommandNumber = commandNumber;
        }

        // Optional: You can add additional constructors to handle inner exceptions or custom messages
        public CommandNumberOutOfRangeException(int commandNumber, string message)
            : base(message)
        {
            CommandNumber = commandNumber;
        }

        public CommandNumberOutOfRangeException(int commandNumber, string message, Exception innerException)
            : base(message, innerException)
        {
            CommandNumber = commandNumber;
        }
    }
}
