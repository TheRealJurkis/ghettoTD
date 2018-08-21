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
    //one of the main Components of the game, manifestation of the mouse
    // and its whereabouts 
    // TODO: will be responsible for choosing states as well as managing towers from sidepanel
    

    class Player
    {
        private RectangleShape HooverMouse;
        private int tilesize, wwidth, wheight, PanelWidth;
        public bool WantsToBuild { get; set; }
        public Tile SelectedTile { get; private set; }


        public Player(int width,int height,int tilesize,int panelwidth)
        {
            this.tilesize = tilesize;
            wwidth = width;
            wheight = height;
            WantsToBuild = false;
            PanelWidth = panelwidth;

            HooverMouse = new RectangleShape();
            HooverMouse.Size = new Vector2f(tilesize,tilesize);
            HooverMouse.FillColor = new Color(0, 0, 200, 125);

            SelectedTile = new Tile(0, 0, tilesize);
            SelectedTile.Tvar.FillColor = new Color(230, 0, 0, 125);
        }
   
        private void CheckIfTileSwitch(RenderWindow okno)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                SelectedTile.Tvar.Position = HooverMouse.Position;
            }            
        }

        public void Draw(RenderWindow okno)
        {
            okno.Draw(HooverMouse);
            okno.Draw(SelectedTile.Tvar);
        }

        public void Update(RenderWindow okno)
        {
            MouseHooverUpdate(okno);
            CheckIfTileSwitch(okno);
        }

        public void MouseHooverUpdate(RenderWindow okno)
        {
            //refactor and improve selection
            //add more functions
            //EVENTS FOR FUCKS SAKE


            int X = Mouse.GetPosition(okno).X / tilesize;
            int Y = Mouse.GetPosition(okno).Y / tilesize;

            if (X == wwidth / tilesize)
            {
                Console.WriteLine(X);
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    WantsToBuild = true;
                    return;
                }
            }
            else if (X > wwidth / tilesize)
            { X = (wwidth / tilesize) -1; }

            if (Y >= wheight / tilesize) { Y = (wheight / tilesize) -1; }
            if (X < 0) { X = 0; }
            if (Y < 0) { Y = 0; }
     

            HooverMouse.Position = new Vector2f(X * tilesize, Y * tilesize);
        }
    }
}
