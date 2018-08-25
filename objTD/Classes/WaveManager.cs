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
    //manages wave and releases in set intervals
    // should check if all enemies are dead maybe give rewards for completing a level


    class WaveManager
    {
        const int TileSize = 32;
        List<Wave> WaveList;
        public Wave CurrentWave { get; private set; }
        
        Clock WaveManagerClock;
        int AMOUNTOFLEVELS = 100;
        public PathFinding.PathGrid Grid;

        public byte WaveLevel { get; set; }




        public WaveManager()
        {
            //hardoced tielsize
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
            //all should be dead;

            Wave wave = new Wave();
            wave.FillWave(WaveLevel++);
            Console.WriteLine(WaveLevel);
            WaveList.Add(wave);

            CurrentWave = WaveList.ElementAt(WaveList.Count - 1);
            WaveList.RemoveAt(WaveList.Count - 1);
        }

        private bool WaveValve(Clock wmc)
        {
            //and everyone from previous dead...
            if ( CurrentWave.AllDead || CurrentWave==null ) //Currentwave.alldead
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
            //Console.WriteLine(CurrentWave.AllDead);
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
