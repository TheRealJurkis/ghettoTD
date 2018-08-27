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
    /*manages wave and releases in set intervals
     * 
     * 
     * TODO: give rewards for completing level..
     */

    class WaveManager
    {
        const int TileSize = 32;
        List<Wave> WaveList;
        public Wave CurrentWave { get; private set; }
        
        Clock WaveManagerClock;
        int AMOUNTOFLEVELS = 100;
        public PathFinding.PathGrid Grid;

        public byte WaveLevel { get; set; }

        public event DeathHandler OnDeath;
        public event LifeDownHandler OnLifeDown;
        public delegate void LifeDownHandler(WaveManager wm, EventArgs e);
        public delegate void DeathHandler(WaveManager wm, EventArgs e);


        public WaveManager()
        {
            WaveManagerClock = new Clock();
            WaveList = new List<Wave>();
            WaveLevel = 1;
            ReleaseWave();
        }
        public void UpdateGrid(PathFinding.PathGrid m)
        {
            Grid = m;
        }

        private void FillGame()
        {
            for (int level = AMOUNTOFLEVELS ; 1 <= level; level--)
            {
                Wave wave = new Wave();
                wave.FillWave(level); // manages what type of enemies come in
                WaveList.Add(wave);
            }
        }

        private void ReleaseWave()
        {
            Wave wave = new Wave();
            wave.FillWave(WaveLevel++);
            Subscribe(wave);
            CurrentWave = wave;

        }

        public void Subscribe(Wave w)
        {
            w.dead += new Wave.DeadHandler(EnemyDied);
            w.EnemyEscape += new Wave.EnemyEscapeHandler(Escaped);
        }
        public void EnemyDied(Wave w,EventArgs e)
        {
            OnDeath(this, EventArgs.Empty);
        }
        private void Escaped(Wave w, EventArgs e)
        {
            OnLifeDown(this, EventArgs.Empty);
        }

        private bool WaveValve(Clock wmc)
        {
            //and everyone from previous dead...
            if ( CurrentWave.AllDead || CurrentWave==null )
            {
                ReleaseWave();
                return true;
            }         
            else{ return false; }
        }

        public void Update()
        {

            if (CurrentWave == null)
            {
                return;
            }
            if (CurrentWave.AllDead)
            {
                WaveManagerClock.Restart();
            }

            WaveValve(WaveManagerClock);
            CurrentWave.Update(Grid);
        }

        public void Draw(RenderWindow okno)
        {
            if (CurrentWave == null) { return; }
            CurrentWave.Draw(okno);
        }
    }
}
