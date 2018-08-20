using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;


namespace objTD.Classes
{
    //SFML does not have vector methods needed and is sealed,
    // So this class was created.. 
    //TODO: rethink

    public class Vector2DUtil
    {
        //this is a horrible idea...
        public Vector2DUtil()
        {
        }

        public Vector2f NormalizeVector(Vector2f v)
        {
            float x, y;
            if (v.X == 0) { x = 0; }
            else { x = v.X / Math.Abs(Length(v)); }
            if (v.Y == 0) { y = 0; }
            else { y = v.Y / Math.Abs(Length(v)); }
            return new Vector2f(x, y);
        }

        public float Length(Vector2f v)
        {
            return (float)Math.Sqrt(v.X * v.X + v.Y*v.Y);
        }

        public float Distance(Vector2f v,Vector2f u)
        {
            return Length(v - u);
        }

        public float Dot(Vector2f u, Vector2f v)
        {
            return u.X * v.X + u.Y * v.Y;
        }

        public float AngleBetweenVectors(Vector2f v, Vector2f u)
        {
            float lengthu = Length(u);
            float lengthv = Length(v);

            float Dp = Dot(v, u);

            if (Dp == 0)
            { return 90; }
            if(Dp == lengthu*lengthv)
            { return 0; }

            if(lengthv == 0 || lengthu == 0)
            {
                return 90;
            }

            float rad = (float)Math.Acos(Dp / (lengthu * lengthv));
            return rad * (float)(180 / Math.PI);
        }



    }
}
