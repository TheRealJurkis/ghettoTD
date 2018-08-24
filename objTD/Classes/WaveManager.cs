using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        Tile SpawnTile;
        public PathFinding.PathGrid Grid;

        public WaveManager()
        {
            //hardoced tielsize
            WaveManagerClock = new Clock();
            WaveList = new List<Wave>();
            SpawnTile = new Tile(0, 8, TileSize);
            FillGame();
        }
        public void UpdateGrid(PathFinding.PathGrid m)
        {
            Grid = m;
        }

        private void FillGame()
        {
            for (int level = 0; level <= AMOUNTOFLEVELS; level++)
            {
                Wave wave = new Wave(SpawnTile);
                wave.FillWave(10); // manages what type of enemies come in
                WaveList.Add(wave);
            }
        }

        public void ReleaseWave()
        {
            //all should be dead or more waves at once
            if (WaveList.Count == 0) return;
            CurrentWave = WaveList.ElementAt(WaveList.Count - 1);
            WaveList.RemoveAt(WaveList.Count - 1);
        }

        private bool WaveValve(Clock wmc)
        {
            //and everyone from previous dead...
            if (wmc.ElapsedTime.AsSeconds() >= 5 && true ) //Currentwave.alldead
            {
                wmc.Restart();
                ReleaseWave();
                return true;
            }         
            else{ return false; }
        }

        public void Update()
        {
            //nejako zmerat cas a potom pustit
            //CheckForDeaths();

            if (WaveValve(WaveManagerClock))
            {
                WaveManagerClock.Restart();
            }
            if (CurrentWave == null)
            {
                return;
            }
                CurrentWave.Update(Grid);
        }

        public void Draw(RenderWindow okno)
        {
            if (CurrentWave == null) { return; }
            CurrentWave.Draw(okno);
        }
    }
}
