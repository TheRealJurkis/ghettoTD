using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace objTD.Classes
{
    class Game
    {
        /*Game creates tiles for the map
         * has battlecomponent and Pathfinder
         *updates and draws its components,
         *
         * TODO: Improve naming and hardcoded...
         */

        PathFinding.PathGrid Grid;
        Player player;
        //SidePanel sp;
        BattleComponent battlecomponent;
        Pathfinder finder;

        public event PauseHandler PauseGame;
        public delegate void PauseHandler(Game g, EventArgs e);

        public event PauseHandler TurnOffGame;

        public void Subscribe()
        {
            battlecomponent.GameOver +=new BattleComponent.GameOverHandler(OnGameOver);
        }
        private void OnGameOver(BattleComponent bc, EventArgs e)
        {
            Console.WriteLine("gg");
            TurnOffGame(this, EventArgs.Empty);
        }


        //Too many parameters
        public Game(int width,int height,int tilesize,PathFinding.PathGrid grid,int panelwidth)
        {
            Grid = grid;

            player = new Player(width,height,panelwidth);
            player.GiveSelectedNode((new Location(0, 0)));
            battlecomponent = new BattleComponent();
            battlecomponent.UpdateGrids(grid);
            Subscribe();

            //sp = new SidePanel(width,panelwidth,height,tilesize);

            finder = new Pathfinder();
            Grid.UpdateFlowGrid(finder.CalculateFlowGrid(Grid));
        }
        private void UpdateGameGrid()
        {
            Grid = battlecomponent.tm.Grid;
            if(battlecomponent.tm.TowerWasBuilt)
            {
                Grid.UpdateFlowGrid(finder.CalculateFlowGrid(Grid));
                battlecomponent.tm.UpdateGrid(Grid);
                battlecomponent.wm.UpdateGrid(Grid);
            }
        }

        public void Update(RenderWindow okno)
        {
            //updates player position 
            //selected tiles and towers 
            player.Update(okno);

            battlecomponent.Update(player);
            battlecomponent.UpdateGrids(Grid);

            UpdateGameGrid();

            if(Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                PauseGame?.Invoke(this, EventArgs.Empty);
            }

        }

        public void Draw(RenderWindow okno)
        {
            Grid.Draw(okno);
            player.Draw(okno);
            battlecomponent.Draw(okno);
            //sp.Draw(okno);
        }
    }
}
