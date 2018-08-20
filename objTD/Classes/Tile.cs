using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;


namespace objTD.Classes
{
    class Tile
    {
        //basic building block for the tile map
        // TODO: rethink accessors


        public RectangleShape Tvar;
        public Tile(int x, int y, int size)
        {

            Tvar = new RectangleShape();
            Tvar.Position = new Vector2f(x * size, y * size);
            Tvar.Size = new Vector2f(size, size);

        }
        public void GiveTexture(Texture t)
        {
            this.Tvar.Texture = t;
        }

        public int GetGridPositionX()
        {
            return (int)Tvar.Position.X / (int)this.Tvar.Size.X;
        }
        public int GetGridPositionY()
        {
            return (int)Tvar.Position.Y / (int)this.Tvar.Size.Y;
        }
        public bool Buildable { get; set; }


    }
}
