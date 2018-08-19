using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using SFML.System;
using SFML.Graphics;
namespace objTD.Classes
{
   abstract class Entity
    {
        public Vector2f Position { get; set; }


        public Entity()
        {
            
        }

        public abstract void Update();

        public abstract void Draw();

    }
}
