using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace objTD.Classes.PathFinding
{



    class PathNode
    {
        const int TileSize = 32;
        private List<PathNode> Neighbors;
        public Location NodeLocation { get; set; }

        public bool Buildable { get; set; }
        public bool Walkable { get; set; }
        public byte Cost { get; set; }
        public RectangleShape TileNode;
        public Vector2f NodeFlow { get; set; }
        public bool Integrated { get; set; }


        public PathNode(Location loc)
        {
            Integrated = false;
            NodeLocation = loc;
            Neighbors = new List<PathNode>();
            Walkable = true;
            Buildable = true;
        }
        public void SetupNode(Texture text)
        {
            TileNode = new RectangleShape();
            TileNode.Size = new Vector2f(TileSize,TileSize);
            TileNode.Position = new Vector2f(this.NodeLocation.x * TileSize, this.NodeLocation.y * TileSize);
            TileNode.Texture = text;
        }

        public void GiveNeighbor(PathNode node)
        {
            Neighbors.Add(node);
        }
        public void LightUp()
        {
            TileNode.FillColor = new Color(50, 0, 0, 50);
        }

        public void GiveTexture(Texture t)
        {
            TileNode.Texture = t;
        }

        public List<PathNode> GetNeighbors()
        {
            return Neighbors;
        }
    }
}
