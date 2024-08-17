using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    public enum ElementSearchType : int
    {
        /// <summary>
        /// Search element with depth-first search (DFS)
        /// </summary> 
        DFS = 0,
        /// <summary>
        /// Search element with breadth-first search (BFS)
        /// </summary>
        BFS = 1,
    }
}
