using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace objTD.Classes.PathFinding
{
    class PathGrid
    {
        public int Xwidth, Ywidth,TileSize;
        private Texture text;
        private Texture finish;
        private PathNode[,] pathgrid;
        public PathNode Exit { get; private set; }

        public PathGrid(int XWindowWidth, int YWindowWidth, int TileSize)
        {
            text = new Texture("grass.jpg");
            finish = new Texture("dirt.jpg");
            Xwidth = XWindowWidth/TileSize;
            Ywidth = YWindowWidth/TileSize;
            this.TileSize = TileSize;
            pathgrid = new PathNode[Xwidth,Ywidth];

            //Creates nodes
            for (int i = 0; i < Xwidth; i++)
            {
                for (int j = 0; j < Ywidth; j++)
                {
                    PathNode node = new PathNode(new Location(i, j));
                    node.SetupNode(text);
                    node.Walkable = true;
                    node.Buildable = true;
                    node.Cost = 1;
                    pathgrid[i, j] = node;
                }
            }

            for (int i = 0; i < Xwidth; i++)
            {
                for (int j = 0; j < Ywidth; j++)
                {
                    UpdateNodeEdges(i,j);
                }
            }
            Exit = pathgrid[Xwidth - 1, Ywidth / 2 - 1];
            pathgrid[Xwidth - 1, Ywidth / 2 - 1].Buildable = false;
            pathgrid[Xwidth - 1, Ywidth / 2 - 1].GiveTexture(finish);

        }
        public PathNode GetPathNode(Location loc)
        {
            if(loc.x >= 0 && loc.x<= Xwidth-1 && loc.y >=0 && loc.y <= Ywidth-1)
            {
                return pathgrid[loc.x, loc.y];
            }
            else
            {
                return null;
            }
        }

        public List<PathNode> GetNodeNeighbors(Location loc)
        {
            return pathgrid[loc.x, loc.y].GetNeighbors();
        }

        public void UpdateNodeEdges(int x, int y)
        {
            PathNode node = pathgrid[x, y];
            //make possible edges

            //rohy
            if((node.NodeLocation.x == 0) && (node.NodeLocation.y == 0))
            {
                node.GiveNeighbor(pathgrid[0, 1]);
                //node.GiveNeighbor(pathgrid[1, 1]);
                node.GiveNeighbor(pathgrid[1, 0]);
            }
            else if ((node.NodeLocation.x == 0) && (node.NodeLocation.y == Ywidth -1))
            {
                node.GiveNeighbor(pathgrid[0, Ywidth-2]);
                node.GiveNeighbor(pathgrid[1, Ywidth-1]);
                //node.GiveNeighbor(pathgrid[1, Ywidth-2]);
            }
            else if ((node.NodeLocation.x == Xwidth-1) && (node.NodeLocation.y == Ywidth-1))
            {
                node.GiveNeighbor(pathgrid[Xwidth-1, Ywidth-2]);
                //node.GiveNeighbor(pathgrid[Xwidth-2, Ywidth-2]);
                node.GiveNeighbor(pathgrid[Xwidth-2, Ywidth-1]);
            }
            else if ((node.NodeLocation.x == Xwidth-1) && (node.NodeLocation.y == 0))
            {
                node.GiveNeighbor(pathgrid[Xwidth - 2, 0]);
                node.GiveNeighbor(pathgrid[Xwidth - 1, 1]);
                //node.GiveNeighbor(pathgrid[Xwidth - 2, 1]);
            }
            //Okraje
            //left
            else if ((node.NodeLocation.x==0) && (node.NodeLocation.y!=0) && (node.NodeLocation.y!=Ywidth-1))
            {
                node.GiveNeighbor(pathgrid[x, y - 1]);
                node.GiveNeighbor(pathgrid[x, y + 1]);
                node.GiveNeighbor(pathgrid[x+1, y]);
                //node.GiveNeighbor(pathgrid[x+1, y - 1]);
                //node.GiveNeighbor(pathgrid[x+1, y + 1]);

            }
            //up
            else if ((node.NodeLocation.y == 0) && (node.NodeLocation.x != 0) && (node.NodeLocation.x != Xwidth - 1))
            {
                node.GiveNeighbor(pathgrid[x - 1, y]);
                node.GiveNeighbor(pathgrid[x + 1, y]);
                //node.GiveNeighbor(pathgrid[x - 1, y+1]);
                //node.GiveNeighbor(pathgrid[x + 1, y + 1]);
                node.GiveNeighbor(pathgrid[x, y + 1]);
            }
            //right
            else if ((node.NodeLocation.x == Xwidth-1) && (node.NodeLocation.y != 0) && (node.NodeLocation.y != Ywidth - 1))
            {
                node.GiveNeighbor(pathgrid[x - 1, y    ]);
                node.GiveNeighbor(pathgrid[x    , y + 1]);
                node.GiveNeighbor(pathgrid[x    , y - 1]);
                //node.GiveNeighbor(pathgrid[x - 1, y - 1]);
                //node.GiveNeighbor(pathgrid[x - 1, y + 1]);

            }
            //down
            else if ((node.NodeLocation.y == Ywidth-1) && (node.NodeLocation.x != 0) && (node.NodeLocation.x != Xwidth - 1))
            {
                node.GiveNeighbor(pathgrid[x - 1, y]);
                node.GiveNeighbor(pathgrid[x +1, y ]);
                //node.GiveNeighbor(pathgrid[x -1, y - 1]);
                //node.GiveNeighbor(pathgrid[x +1, y - 1]);
                node.GiveNeighbor(pathgrid[x , y-1]);

            }
            //centre
            else
            {
               // node.GiveNeighbor(pathgrid[x + 1, y -1]);
                node.GiveNeighbor(pathgrid[x, y -1]);
               // node.GiveNeighbor(pathgrid[x - 1, y - 1]);

                node.GiveNeighbor(pathgrid[x - 1, y]);
                node.GiveNeighbor(pathgrid[x+1, y]);

                node.GiveNeighbor(pathgrid[x, y +1]);
                //node.GiveNeighbor(pathgrid[x+1, y + 1]);
                //node.GiveNeighbor(pathgrid[x-1, y+- 1]);

            }
            pathgrid[x, y] = node;
        }


        public void UpdateFlowGrid(Vector2f[,] FlowGrid)
        {
            for (int i = 0; i < Xwidth; i++)
            {
                for (int j = 0; j < Ywidth; j++)
                {
                    pathgrid[i, j].NodeFlow = FlowGrid[i, j];
                }
            }

        }

        public void MarkUnwalkable(Location loc)
        {
            pathgrid[loc.x, loc.y].Walkable = false;
        }
        public void MarkWalkable(Location loc)
        {
            pathgrid[loc.x, loc.y].Walkable = true;
        }
        public void MarkBuildable(Location loc)
        {
            pathgrid[loc.x, loc.y].Buildable = true;
        }
        public void MarkUnBuildable(Location loc)
        {
            pathgrid[loc.x, loc.y].Buildable = false;
        }

        public void Draw(RenderWindow okno)
        {
            for (int i = 0; i < Xwidth; i++)
            {
                for (int j = 0; j < Ywidth; j++)
                {
                    okno.Draw(pathgrid[i, j].TileNode);
                }
            }

        }

    }
}
