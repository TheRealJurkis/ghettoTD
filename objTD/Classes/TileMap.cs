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

        private Clock clk;
        public TileMap(int WWidth, int Wheight, int TileSize)
        {
            //initializes the TileArray
            clk = new Clock();
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

        public bool CheckIfBuildable(int x, int y)
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
            //berie mapu intigrov  1== unwalkable
            //ghetto editor
            for (int i = 0; i < TileMapWidth; i++)
            {
                for (int j = 0; j < TileMapHeight; j++)
                {
                    //zmenit na enum maybe
                    if (map[i][j] == 1)
                    {
                        TileArray[i][j].GiveTexture(DirtTexture);
                        TileArray[i][j].Buildable = false;
                    }
                    else
                    {
                        TileArray[i][j].GiveTexture(GrassTexture);
                        TileArray[i][j].Buildable = true;
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
            //bug when building outside of window;
            return TileArray[x][y];
        }

        public void Draw(RenderWindow okno)
        {
           // clk.Restart();
            for (int i = 0; i < TileMapWidth; i++)
            {
                for (int j = 0; j < TileMapHeight; j++)
                {
                    okno.Draw(TileArray[i][j].Tvar);
                }
            }
           // Console.WriteLine(clk.ElapsedTime.AsMilliseconds());
        }
    }
}
