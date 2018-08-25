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
    //This class is intended to provide tower management
    //like building,destroying,updates and draws

    class TowerManager
    {
        private List<Tower> TowerList;
        public PathFinding.PathGrid Grid;
        PathFinding.PathNode CurrentNode;
        private Clock clk;
        public bool TowerWasBuilt { get; set; }

        public TowerManager()
        {
            TowerList = new List<Tower>();
            clk = new Clock();
        }

        public List<Tower> GetTowers()
        {
            return TowerList;
        }

        public void BuildTower(Location loc ,TowerTypes type)
        {
            //zmenit tower
            PathFinding.PathNode node = Grid.GetPathNode(loc);

            TowerList.Add(new Tower(node,type)); //towertype
            node.Buildable = false;
            node.Walkable = false;
            node.Cost = 255;
            TowerWasBuilt = true;

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
            Console.WriteLine(Grid.GetPathNode(CurrentNode.NodeLocation).NodeFlow.ToString());
        }
        public void Draw(RenderWindow okno)
        {
            //clk.Restart();
            for (int i = TowerList.Count - 1; i >= 0; i--)
            {
                TowerList.ElementAt(i).Draw(okno);
            }
           // Console.WriteLine(clk.ElapsedTime.AsMilliseconds());
        }
    }
}
