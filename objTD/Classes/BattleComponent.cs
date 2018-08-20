using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using SFML.Window;
using SFML.System;
using SFML.Graphics;
namespace objTD.Classes
{
    class BattleComponent
    {
        TowerManager tm;
        WaveManager wm;



        public BattleComponent()
        {
            tm = new TowerManager();
            wm = new WaveManager();

        }


        public void LoadMap(TileMap map)
        {
            tm.LoadMap(map);
        }

        public void CollisionCheck()
        {

        }

        public void Update(Tile tile)
        {
            tm.UpdateCurrentTile(tile);
            tm.Update();
            wm.Update();

        }
        public void Draw(RenderWindow okno)
        {
            tm.Draw(okno);
            wm.Draw(okno);
        }


    }
}
