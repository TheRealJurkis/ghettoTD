using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace objTD.Classes
{
    

    class Player
    {
        RectangleShape HooverMouse;
        int tilesize,wwidth,wheight;

        public Tile selectedtile;



        public Player(int width,int height,int tilesize)
        {
            this.tilesize = tilesize;
            HooverMouse = new RectangleShape();
            HooverMouse.Size = new Vector2f(tilesize,tilesize);
            wwidth = width;
            wheight = height;
            selectedtile = new Tile(0, 0, tilesize);
            selectedtile.Tvar.FillColor = new Color(230, 0, 0, 125);


            HooverMouse.FillColor = new Color(0,0,200,125);
        }
   
        private void CheckIfTileSwitch(RenderWindow okno)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                selectedtile.Tvar.Position = HooverMouse.Position;
            }            
        }

        public void Draw(RenderWindow okno)
        {
            okno.Draw(HooverMouse);
            okno.Draw(selectedtile.Tvar);

        }


        public void Update(RenderWindow okno)
        {
            CheckIfTileSwitch(okno);
            MouseHooverUpdate(okno);
        }


        public void MouseHooverUpdate(RenderWindow okno)
        {
            int X = Mouse.GetPosition(okno).X / tilesize;
            int Y = Mouse.GetPosition(okno).Y / tilesize;
            if (X > wwidth / tilesize) { X = wwidth / tilesize; }
            if (Y > wheight / tilesize) { Y = wheight / tilesize; }
            if (X < 0) { X = 0; }
            if (Y < 0) { Y = 0; }


            HooverMouse.Position = new Vector2f(X * tilesize, Y * tilesize);
        }
    }
}
