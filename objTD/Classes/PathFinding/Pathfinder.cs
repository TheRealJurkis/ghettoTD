using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;

namespace objTD.Classes
{
    class Pathfinder
    {


        public Pathfinder()
        {

        }
        private byte[,] GetCostField(PathFinding.PathGrid grid)
        {
            byte[,] CostField = new byte[grid.Xwidth, grid.Ywidth];


            for (int i = 0; i < grid.Xwidth; i++)
            {
                for (int j = 0; j < grid.Ywidth; j++)
                {
                    CostField[i, j] = grid.GetPathNode(new Location(i, j)).Cost; 
                }
            }
            return CostField;
        }

        public Vector2f[,] CalculateFlowGrid(PathFinding.PathGrid grid)
        {
            //Calculates CostGrid

            byte[,] CostField = GetCostField(grid);
            int[,] IntegrationField = new int[grid.Xwidth, grid.Ywidth];
            //init integrationfield
            for (int i = 0; i < grid.Xwidth; i++)
            {
                for (int j = 0; j < grid.Ywidth; j++)
                {
                    IntegrationField[i, j] = int.MaxValue;
                }
            }
            PathFinding.PathNode exit = grid.Exit;
            CostField[exit.NodeLocation.x, exit.NodeLocation.y] = 0;

            IntegrationField[exit.NodeLocation.x, exit.NodeLocation.y] = 0;

            Queue<PathFinding.PathNode> Open = new Queue<PathFinding.PathNode>();
            HashSet<PathFinding.PathNode> closed = new HashSet<PathFinding.PathNode>();
            Open.Enqueue(exit);

            while (Open.Count != 0)
            {
                PathFinding.PathNode current = Open.Dequeue();

                List<PathFinding.PathNode> neighbors = current.GetNeighbors();
                

                //BFS

                foreach (PathFinding.PathNode item in neighbors)
                {
                    if(CostField[item.NodeLocation.x,item.NodeLocation.y]==255 && !closed.Contains(item))
                    {
                        continue;
                    }
                    

                    int ItemIntegrationCost = IntegrationField[item.NodeLocation.x, item.NodeLocation.y];
                    int CurrentCost = CostField[current.NodeLocation.x, current.NodeLocation.y];
                    int ItemCost = CostField[item.NodeLocation.x, item.NodeLocation.y];
                    int NewCost = CurrentCost + ItemCost;



                    if (ItemIntegrationCost > NewCost)
                    {
                        
                        IntegrationField[item.NodeLocation.x, item.NodeLocation.y] = NewCost;
                        if(!Open.Contains(item))
                        {
                            Open.Enqueue(item);
                        }
                    }
                    closed.Add(current);
                }
            }

            int x;
            for (int i = 0; i < grid.Ywidth; i++)
            {
                for (int j = 0; j < grid.Xwidth; j++)
                {
                    if (CostField[j, i] == byte.MaxValue)
                    {
                        x = 0;
                    }
                    else
                    {
                        x = CostField[j, i];
                    }
                    Console.Write("{0}", x);
                }
                Console.WriteLine();
            }






            //Calculates FLowGrid

            Vector2f[,] FlowGrid = new Vector2f[grid.Xwidth, grid.Ywidth];

            for (int i = 0; i < grid.Ywidth; i++)
            {
                for (int j = 0; j < grid.Xwidth; j++)
                {
                    Location loc = new Location(j, i);
                    Location Temp = new Location(0, 0);
                    int IntegrationMin = int.MaxValue;

                    foreach (PathFinding.PathNode node in grid.GetNodeNeighbors(loc))
                    {
                        if(IntegrationField[node.NodeLocation.x,node.NodeLocation.y] <= IntegrationMin)
                        {
                            IntegrationMin = IntegrationField[node.NodeLocation.x, node.NodeLocation.y];
                            Temp = node.NodeLocation;
                        }
                    }

                    Vector2f v = CalculateDirection(loc, Temp);

                    FlowGrid[j, i] = v;
                }
            }
            return FlowGrid;
        }

        private Vector2f CalculateDirection(Location from,Location to)
        {
            Vector2f u = new Vector2f(from.x, from.y);
            Vector2f v = new Vector2f(to.x, to.y);

            return v-u;
        }
    }
}
