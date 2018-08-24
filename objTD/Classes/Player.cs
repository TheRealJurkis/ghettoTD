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
        const int TileSize = 32;
        private Location HooverMouse;
        private int  wwidth, wheight, PanelWidth;
        public bool WantsToBuild { get; set; }
        public TowerTypes TowerQueued { get; set; }
        public Location SelectedNode { get; set; }
        public RectangleShape Hoov;
        public RectangleShape Select;

        //ToDO tower queued different tower built

        public Player(int width,int height,int panelwidth)
        {
            wwidth = width;
            wheight = height;
            WantsToBuild = false;
            PanelWidth = panelwidth;
            Hoov = new RectangleShape();
            Select = new RectangleShape();
            Hoov.Size = new Vector2f(32,32);
            Select.Size = Hoov.Size;
            Hoov.FillColor = new Color(130, 0, 0, 80);
            Select.FillColor = new Color(0, 0,130, 80);

        }
   
        public void GiveSelectedNode(Location loc)
        {
            HooverMouse = loc;
            SelectedNode = loc;
        }
        private void CheckIfTileSwitch(RenderWindow okno)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                SelectedNode = HooverMouse;
            }            
        }

        public void Draw(RenderWindow okno)
        {
            Hoov.Position = new Vector2f(HooverMouse.x * TileSize, HooverMouse.y * TileSize);
            Select.Position = new Vector2f(SelectedNode.x * TileSize, SelectedNode.y * TileSize);
            okno.Draw(Hoov);
            okno.Draw(Select);

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
            WantsToBuild = false;

            int X = Mouse.GetPosition(okno).X / TileSize;
            int Y = Mouse.GetPosition(okno).Y / TileSize;

            if (X == wwidth / TileSize)
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    TowerQueued = TowerTypes.Laser;
                    WantsToBuild = true;
                    return;
                }
            }
            else if (X > wwidth / TileSize)
            { X = (wwidth / TileSize) -1; }

            if (Y >= wheight / TileSize) { Y = (wheight / TileSize) -1; }
            if (X < 0) { X = 0; }
            if (Y < 0) { Y = 0; }

            HooverMouse = new Location(X, Y);

        }
    }
}
