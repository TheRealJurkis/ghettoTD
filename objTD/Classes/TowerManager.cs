using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;
using SFML.System;


namespace objTD.Classes
{
    //This class is intended to provide tower management
    //like building,destroying,updates and draws

    class TowerManager
    {
        private List<Tower> TowerList;
        TileMap map;
        Tile CurrentTile;
        private Clock clk;

        public TowerManager()
        {
            TowerList = new List<Tower>();
            clk = new Clock();
        }


        public List<Tower> GetTowers()
        {
            return TowerList;
        }



        public void BuildTower(int x,int y)
        {
            TowerList.Add(new Tower(CurrentTile.GetGridPositionX(), CurrentTile.GetGridPositionY(), 400));
            map.TileArray[CurrentTile.GetGridPositionX()][CurrentTile.GetGridPositionY()].Buildable = false;
        }

        public void LoadMap(TileMap m)
        {
            map = m;
        }

        public void UpdateCurrentTile(Tile tile)
        {
            CurrentTile = map.GetTile(tile.GetGridPositionX(),tile.GetGridPositionY());
        }

        private bool CheckBuild()
        {

            if (CurrentTile != null &&CurrentTile.Buildable)
            {
                return true;
            }
            else return false;
        }

        public void Update(Player player)
        {
            if (CurrentTile != null && CheckBuild() && player.WantsToBuild)
            {
                //postav danu vezu
                BuildTower(CurrentTile.GetGridPositionX(),CurrentTile.GetGridPositionY());
            }

            for (int i = TowerList.Count - 1; i >= 0; i--)
            {
                TowerList.ElementAt(i).Update();
            }

        }
        public void Draw(RenderWindow okno)
        {
            //clk.Restart();
            for (int i = TowerList.Count - 1; i >= 0; i--)
            {
                TowerList.ElementAt(i).Draw(okno);
            }
           // Console.WriteLine(clk.ElapsedTime.AsMilliseconds());
        }
    }
}
