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
                    //TODO: too slow,  AABB or BOX2D

                    //or try to check distance from tower to enemy since it is just a circle
                    //float dist = V2D.Distance(towers.ElementAt(i).AttackRadius.Position, enemies.ElementAt(j).Manifestation.Position);
                    //if (dist <= towers.ElementAt(i).AttackRadius.Radius)
                    //{
                    //    Console.WriteLine("good");
                    //}
                    //else { Console.WriteLine("bad"); }


                    bool hit = towers.ElementAt(i).AttackRadius.GetGlobalBounds().Intersects(enemies.ElementAt(j).Manifestation.GetGlobalBounds());
                    Console.WriteLine(hit);
                    Console.WriteLine(enemies.ElementAt(j).Manifestation.GetGlobalBounds().ToString());
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
