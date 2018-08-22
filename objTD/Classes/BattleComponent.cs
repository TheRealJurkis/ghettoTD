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
        const int CollisionFrameDelay = 10;

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
            Wave wave = wm.CurrentWave;
            if (wave == null) return;
            List<Enemy> enemies = wave.GetActiveEnemyList();
            List<Tower> towers = tm.GetTowers();

            for (int i = towers.Count -1; i >= 0; i--)
            {
                //save time to check only towers that can shoot
               // if(towers.ElementAt(i).Reloading)
               // { continue; }

                //for (int j = enemies.Count -1 ; j >= 0; j--)
                //{
                for (int j = 0; j < enemies.Count; j++)
                {

                    Tower veza = towers.ElementAt(i);
                    Enemy nepriatel = enemies.ElementAt(j);


                    bool hit = veza.AttackRadius.GetGlobalBounds().Intersects(nepriatel.Manifestation.GetGlobalBounds());

                    if(hit)
                    {
                        veza.Rotate(nepriatel);
                        if(veza.ReadyToShoot)
                        {
                            veza.ShootAt(nepriatel);
                        }
                        hit = false;
                        break;
                    }
                }
            }
        }

        public void Update(Player player)
        {
            tm.UpdateCurrentTile(player.SelectedTile);
            tm.Update(player);
            wm.Update();
            CollisionCheck();

        }
        public void Draw(RenderWindow okno)
        {
            tm.Draw(okno);
            wm.Draw(okno);
        }
    }
}
