using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using SFML.System;
using SFML.Graphics;


namespace objTD.Classes
{
    //groups enemies together to create a wave, fills waves, releases waves
    //TODO: maybe move to wavemanager
    //Two main lists of enemies, dormant enemies which are to be spawned this wave,
    // drawable are those that are being drawn


    class Wave
    {
        List<Enemy> DormantEnemyList;
        List<Enemy> DrawableEnemies;

        //Tile SpawnTile;
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
                DormantEnemyList.Add(new Enemy(level,start));
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
                if(DrawableEnemies.ElementAt(i).Dead)
                {
                    //Finalize or dispose??
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
