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

        Tile SpawnTile;
        public Clock WaveClock;
        int WaveCounter;
        int EnemyIntervalTime = 50;
        bool WaveReleaseFinished;
        public bool AllDead { get; private set; }

        public Wave(Tile spawntile)
        {
            AllDead = false;
            WaveClock = new Clock();
            DormantEnemyList = new List<Enemy>();
            SpawnTile = spawntile;
            DrawableEnemies = new List<Enemy>();
        }

        public void FillWave(int AmountOfEnemies)
        {
            for (int i = 0; i < AmountOfEnemies; i++)
            {
                DormantEnemyList.Add(new Enemy(10));
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

        public void CheckDeadEnemies()
        {
            //iterates through and deletes dead enemies
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

        public void CheckForRelease()
        {
            if ((WaveClock.ElapsedTime.AsMilliseconds() >= EnemyIntervalTime) && !WaveReleaseFinished )
            {
                WaveClock.Restart();

                RealeaseEnemy();
            }
        }

        public void Update()
        {
            //checkDead();
            CheckForRelease();
            for (int i = DrawableEnemies.Count - 1; i >= 0; i--)
            {
                if(DrawableEnemies.ElementAt(i).Dead)
                {
                    //Finalize or dispose??
                    DrawableEnemies.RemoveAt(i);
                    continue;
                }
                DrawableEnemies.ElementAt(i).Update();
                
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
