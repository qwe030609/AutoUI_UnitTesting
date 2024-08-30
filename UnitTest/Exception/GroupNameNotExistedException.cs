using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    public class GroupNameNotExistedException : Exception
    {
        // Custom property to hold the group name
        public string GroupName { get; }

        // Constructor that accepts the group name as a parameter
        public GroupNameNotExistedException(string groupName)
            : base($"GroupName with name '{groupName}' not found.")
        {
            GroupName = groupName;
        }

        // Optional: You can add additional constructors to handle inner exceptions or custom messages
        public GroupNameNotExistedException(string groupName, string message)
            : base(message)
        {
            GroupName = groupName;
        }

        public GroupNameNotExistedException(string groupName, string message, Exception innerException)
            : base(message, innerException)
        {
            GroupName = groupName;
        }
    }
}
