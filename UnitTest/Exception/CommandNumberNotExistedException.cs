using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    public class CommandNumberNotExistedException : Exception
    {
        public CommandNumberNotExistedException(string msg) : base(msg) 
        { 

        }

        //public CommandNumberNotExistedException(string msg, Exception innerException) : base(msg, innerException) { }

        // You can add additional properties or methods as needed
    }
}
