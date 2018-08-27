using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objTD.Classes
{
    /* location helper class
    */


    class Location : IEquatable<Location>
    {

        public int x { get; set; }
        public int y { get; set; }

        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Location);
        }

        public bool Equals(Location l)
        {

            if (Object.ReferenceEquals(l, null))
            {
                return false;
            }


            if (Object.ReferenceEquals(this, l))
            {
                return true;
            }

            if (this.GetType() != l.GetType())
            {
                return false;
            }

            // Return true if the fields match.

            return (x == l.x) && (y == l.y);
        }


        public static bool operator ==(Location l1, Location l2)
        {
            if (Object.ReferenceEquals(l1, null))
            {
                if (Object.ReferenceEquals(l2, null))
                {

                    return true;
                }
                return false;
            }

            return l1.Equals(l2);
        }
        public static bool operator !=(Location l1, Location l2)
        {
            return !(l1 == l2);
        }

        public override string ToString() => $"[{this.x} , {this.y}]";

    }
}
