using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objTD.Classes.PathFinding
{
    class PathEdge
    {
        public PathNode End { get; private set; }


        public PathEdge(PathNode end)
        {
            this.End = end;
        }

    }
}
