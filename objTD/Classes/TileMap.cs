using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;

namespace objTD.Classes
{
    class TileMap
    {
        //Map class, resposinble for building the levelground
        //and drawing backround

        private Texture GrassTexture, DirtTexture;
        private int TileMapWidth, TileMapHeight;
        public Tile[][] TileArray;
        private int TileSize;

        //redundacy passing parameters over and over again
        public TileMap(int WWidth, int Wheight, int TileSize)
        {
            //initializes the TileArray
            this.TileSize = TileSize;
            TileMapHeight = Wheight / TileSize;
            TileMapWidth = WWidth / TileSize;
            TileArray = new Tile[TileMapWidth][];
            for (int i = 0; i < TileMapWidth; i++)
            {
                TileArray[i] = new Tile[TileMapHeight];
            }
            BuildTiles();
        }
        private void BuildTiles()
        {
            for (int i = 0; i < TileMapWidth; i++)
            {
                for (int j = 0; j < TileMapHeight; j++)
                {
                    TileArray[i][j] = new Tile(i, j, TileSize);
                }
            }
        }
        //propg
        public bool TileBuildable(int x, int y)
        {
            return TileArray[x][y].Buildable;
        }

        //poorname
        public void LoadTextures(string buildablepath, string unbuildablepath)
        {
            GrassTexture = new Texture(buildablepath);
            DirtTexture = new Texture(unbuildablepath);
        }

        public void BuildMap(int[][] map)
        {
            //berie mapu intigrov 
            // 1== unwalkable
            // 2== up down point
            // 3== right point
            for (int i = 0; i < TileMapWidth; i++)
            {
                for (int j = 0; j < TileMapHeight; j++)
                {
                    switch (map[i][j])
                    {
                        case 1:
                            TileArray[i][j].GiveTexture(DirtTexture);
                            TileArray[i][j].Buildable = false;
                            continue;
                        case 2:
                            TileArray[i][j].GiveTexture(DirtTexture);
                            TileArray[i][j].Buildable = false;
                            continue;
                        case 3:
                            TileArray[i][j].GiveTexture(DirtTexture);
                            TileArray[i][j].Buildable = false;
                            continue;
                        case 0:
                            TileArray[i][j].GiveTexture(GrassTexture);
                            TileArray[i][j].Buildable = true;
                            continue;
                    }
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            if(x >= TileMapWidth)
            {
                x = TileMapWidth - 1;
            }
            if(y >= TileMapHeight)
            {
                y = TileMapHeight - 1;
            }
            return TileArray[x][y];
        }

        public void Draw(RenderWindow okno)
        {
            for (int i = 0; i < TileMapWidth; i++)
            {
                for (int j = 0; j < TileMapHeight; j++)
                {
                    okno.Draw(TileArray[i][j].Tvar);
                }
            }
        }
    }
}
