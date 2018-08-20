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
        int counter;
        Vector2DUtil V2D;

        public BattleComponent()
        {
            tm = new TowerManager();
            wm = new WaveManager();
            V2D = new Vector2DUtil();
        }


        public void LoadMap(TileMap map)
        {
            tm.LoadMap(map);
        }

        public void CollisionCheck()
        { 

            Wave wave=wm.GetCurrentWave();
            if (wave == null) return;
            List<Enemy> enemies = wave.GetActiveEnemyList();
            List<Tower> towers = tm.GetTowers();

            for (int i = towers.Count -1; i >= 0; i--)
            {
                //save time to check only towers that can shoot
               // if(towers.ElementAt(i).Reloading)
               // { continue; }
                for (int j = enemies.Count -1 ; j >= 0; j--)
                {
                    bool hit = towers.ElementAt(i).AttackRadius.GetGlobalBounds().Intersects(enemies.ElementAt(j).Manifestation.GetGlobalBounds());
                    
                    //Console.WriteLine(hit);
                    //Console.WriteLine(enemies.ElementAt(j).Manifestation.GetGlobalBounds().ToString());
                    if(hit)
                    {
                        hit = false;
                        break;
                    }
                }


            }


        }

        public void Update(Tile tile)
        {
            tm.UpdateCurrentTile(tile);
            tm.Update();
            wm.Update();

            //check only every couple of frames to save time
            if (counter++ > 10)
            { CollisionCheck();
                counter = 0;
            }
        }
        public void Draw(RenderWindow okno)
        {
            tm.Draw(okno);
            wm.Draw(okno);
        }


    }
}
