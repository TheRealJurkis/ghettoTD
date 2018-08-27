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
    /* This class was created as a middleman between enemies and towers
     * handles collision, Income and updating the grid
     */

    class BattleComponent
    {
        private int Lives = 10;
        public TowerManager tm;
        public WaveManager wm;

        public BattleComponent()
        {
            tm = new TowerManager();
            wm = new WaveManager();
            Subscribe(wm);
        }

        public event GameOverHandler GameOver;
        public delegate void GameOverHandler(BattleComponent bc, EventArgs e);


        //kill enemies get money

        public void Subscribe(WaveManager wavemanager)
        {
            wavemanager.OnDeath += new WaveManager.DeathHandler(OnEnemyDeath);
            wavemanager.OnLifeDown+= new WaveManager.LifeDownHandler(OnLifeDown);
        }
        private void OnEnemyDeath(WaveManager wavemanager, EventArgs e)
        {
            tm.EarnMoney(wavemanager.WaveLevel *10);
        }
        private void OnLifeDown(WaveManager wavemanager, EventArgs e)
        {
            Console.WriteLine("You have {0} lives left.",--Lives);
            if(Lives<=0)
            {
                GameOver(this, EventArgs.Empty);
            }
        }

        //simple collision check, iterates through every possible collision with enemies and towers
        //need to check this for every tower every frame because of the cannon tracking the enemy

        public void CollisionCheck()
        {
            Wave wave = wm.CurrentWave;
            if (wave == null) return;
            List<Enemy> enemies = wave.GetActiveEnemyList();
            List<Tower> towers = tm.GetTowers();

            for (int i = towers.Count -1; i >= 0; i--)
            {
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


        //work is done outside of BC

        public void UpdateGrids(PathFinding.PathGrid m)
        {
            tm.UpdateGrid(m);
            wm.UpdateGrid(m);
        }

        public void Update(Player player)
        {
            //updates CNode
            tm.UpdateCurrentNode(player.SelectedNode);
            //builds towers
            tm.Update(player);


            //controls waves and enemies
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
