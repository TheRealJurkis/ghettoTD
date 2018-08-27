using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;
using SFML.System;


namespace objTD.Classes
{
    /*This class is intended to provide tower management
     * like building,destroying,updates and draws
     * Also is responsible for moneymanagement which is dumb...
     */
    class TowerManager
    {
        const int StartMoney = 100;


        private List<Tower> TowerList;
        public PathFinding.PathGrid Grid;
        PathFinding.PathNode CurrentNode;
        private Clock clk;
        public int Money { get; set; }
        public bool TowerWasBuilt { get; set; }

        public TowerManager()
        {
            TowerList = new List<Tower>();
            clk = new Clock();
            Money = StartMoney;
            Console.WriteLine("You have {0}$ left...",Money);
        }

        public List<Tower> GetTowers()
        {
            return TowerList;
        }
        public void EarnMoney(int Amount)
        {
            Money += Amount;
        }

        public void BuildTower(Location loc ,TowerTypes type)
        {
            PathFinding.PathNode node = Grid.GetPathNode(loc);

           //hardcoded tower price, change for tower queued

            if(Money >= 50)
            {
                TowerList.Add(new Tower(node, type)); 
                node.Buildable = false;
                node.Walkable = false;
                node.Cost = 255;
                TowerWasBuilt = true;
                Money -= 50;
            }
            Console.WriteLine("You have {0}$ left...", Money);
        }

        public void UpdateGrid(PathFinding.PathGrid m)
        {
            Grid = m;
        }

        public void UpdateCurrentNode(Location loc)
        {
            CurrentNode = Grid.GetPathNode(loc);
        }

        private bool CheckBuild()
        {
            if (CurrentNode != null && CurrentNode.Buildable)
            {
                return true;
            }
            else return false;
        }

        public void Update(Player player)
        {
            if ( CheckBuild() && player.WantsToBuild)
            {
                BuildTower((CurrentNode.NodeLocation),player.TowerQueued);
                TowerWasBuilt = true;
            }
            else
            {
                TowerWasBuilt = false;
            }

            for (int i = TowerList.Count - 1; i >= 0; i--)
            {
                TowerList.ElementAt(i).Update();
            }
        }
        public void Draw(RenderWindow okno)
        {
            for (int i = TowerList.Count - 1; i >= 0; i--)
            {
                TowerList.ElementAt(i).Draw(okno);
            }
        }
    }
}
