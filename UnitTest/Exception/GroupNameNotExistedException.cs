using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    public class GroupNameNotExistedException : Exception
    {
        public GroupNameNotExistedException(string msg) : base(msg)
        {

        }

        //public GroupNameNotExistedException(string msg, Exception innerException) : base(msg, innerException) { }

        // You can add additional properties or methods as needed
    }
}
