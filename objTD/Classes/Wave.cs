using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using SFML.System;
using SFML.Graphics;


namespace objTD.Classes
{
    /* groups enemies together to create a wave, fills waves, releases waves
    * Two main lists of enemies, dormant enemies which are to be spawned this wave,
    * drawable are those that are being drawn */


    class Wave
    {
        //enemies to be added this wave
        public List<Enemy> DormantEnemyList;

        //alive enemies this wave
        public List<Enemy> DrawableEnemies;

        public Clock WaveClock;
        int EnemyIntervalTime = 10;
        bool WaveReleaseFinished;
        public bool AllDead { get; private set; }
        private Random rnd;


        public Wave()
        {
            rnd = new Random();
            AllDead = false;
            WaveClock = new Clock();
            DormantEnemyList = new List<Enemy>();
            DrawableEnemies = new List<Enemy>();



        }

        public void FillWave(int level)
        {
            for (int i = 0; i < level *10 ; i++)
            {
                Vector2f start = new Vector2f(0, rnd.Next(0, 800));
                Enemy e = new Enemy(level, start, (EnemyType)rnd.Next(0, 3));
                DormantEnemyList.Add(e);
            }
        }
        public List<Enemy> GetActiveEnemyList()
        {
            return DrawableEnemies;
        }
        
        public bool EveryoneReleased()
        {
            if (WaveReleaseFinished)
            {
                return true;
            }
            else return false;
        }

        private void RealeaseEnemy()
        {
            if(DormantEnemyList.Count == 0)  //nothing to add
            {
                WaveReleaseFinished = true;
                return;
            }
            DrawableEnemies.Add(DormantEnemyList.ElementAt(DormantEnemyList.Count -1));
            DormantEnemyList.RemoveAt(DormantEnemyList.Count - 1);
        }

        //when an enemy dies, fires and delegates to wavemanager

        public event DeadHandler dead;

        public delegate void DeadHandler(Wave w, EventArgs e);


        //fires when enemy gets to exit location and delegates to wavemanager

        public event EnemyEscapeHandler EnemyEscape;

        public delegate void EnemyEscapeHandler(Wave w, EventArgs e);



        private void CheckForRelease()
        {
            if ((WaveClock.ElapsedTime.AsMilliseconds() >= EnemyIntervalTime) && !WaveReleaseFinished )
            {
                WaveClock.Restart();
                RealeaseEnemy();
            }
        }

        private void CheckIfAllDead()
        {
            if(DrawableEnemies.Count == 0 && WaveReleaseFinished && DormantEnemyList.Count ==0)
            {
                AllDead = true;
            }
        }

        public void Update(PathFinding.PathGrid grid)
        {
            CheckForRelease();
            CheckIfAllDead();

            for (int i = DrawableEnemies.Count - 1; i >= 0; i--)
            {
                Enemy e = DrawableEnemies.ElementAt(i);
                if (e.Dead)
                {
                    //Finalize , dispose??
                    dead(this, EventArgs.Empty);
                    DrawableEnemies.RemoveAt(i);

                    continue;
                }
                if(e.location == grid.Exit.NodeLocation)
                {
                    EnemyEscape?.Invoke(this, EventArgs.Empty);
                    DrawableEnemies.RemoveAt(i);
                    continue;
                }

                DrawableEnemies.ElementAt(i).Update();

                Location loc = DrawableEnemies.ElementAt(i).location;
                DrawableEnemies.ElementAt(i).Move(grid.GetPathNode(loc).NodeFlow);
                
            }
        }

        public void Draw(RenderWindow okno)
        {
            //probably drawing the last one
            for (int i = DrawableEnemies.Count -1; i >= 0; i--)
            {
                DrawableEnemies.ElementAt(i).Draw(okno);
            }
        }
    }
}
