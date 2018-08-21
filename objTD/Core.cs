using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using SFML.Window;
using SFML.Graphics;
using SFML.System;


namespace objTD
{
    /*Core component of the game, Game loop 
     * aswell as the window and game initialization,
     * it shall be moved into startup manager or smth...
     */


    class Core
    {
        static void OnClose(object sender, EventArgs e)
        {
            RenderWindow okno = (RenderWindow)sender;
            okno.Close();
        }

        static void Main(string[] args)
        {
            //should be encapsulated further


            const int PanelWidth = 64;
            int sleep_time = 0;
            const int FPS = 50;
            const int WindowWidth = 1280;
            const int WindowHeight = 960;
            const int TileSize = 64;
            const int SKIP_TICKS = 1000 / FPS;
            const int GridTileSizeX = WindowWidth / TileSize;
            const int GridTileSizeY = WindowHeight / TileSize;


            Clock globalclock = new Clock();
            Clock clock = new Clock();
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 8;
            RenderWindow okno = new RenderWindow(new VideoMode(WindowWidth +PanelWidth, WindowHeight), "okno", Styles.Close, settings);





            //Level editor


            int[][] map = new int[GridTileSizeX][];
            //LEFT
            map[0] = new int[GridTileSizeY]  { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 };
            map[1] = new int[GridTileSizeY]  { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 };
            map[2] = new int[GridTileSizeY]  { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 };
            map[3] = new int[GridTileSizeY]  { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 };
            map[4] = new int[GridTileSizeY]  { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 };
            map[5] = new int[GridTileSizeY]  { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 };
            map[6] = new int[GridTileSizeY]  { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 };
            map[7] = new int[GridTileSizeY]  { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 };
            map[8] = new int[GridTileSizeY]  { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 };
            map[9] = new int[GridTileSizeY]  { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 };  //TOP
            map[10] = new int[GridTileSizeY] { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 };
            map[11] = new int[GridTileSizeY] { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 };
            map[12] = new int[GridTileSizeY] { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 };
            map[13] = new int[GridTileSizeY] { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 };
            map[14] = new int[GridTileSizeY] { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 };
            map[15] = new int[GridTileSizeY] { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 };
            map[16] = new int[GridTileSizeY] { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 };
            map[17] = new int[GridTileSizeY] { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 };
            map[18] = new int[GridTileSizeY] { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 };
            map[19] = new int[GridTileSizeY] { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 };




            Classes.Game game = new Classes.Game(WindowWidth,WindowHeight,TileSize,map,PanelWidth);
            



            //event handles

            okno.Closed += new EventHandler(OnClose);


            clock.Restart();
            while (true)
            {
                //add pause and main menu state
                okno.DispatchEvents();
                okno.Clear();
                game.Update(okno);
                game.Draw(okno);
                okno.Display();
                sleep_time = SKIP_TICKS - clock.Restart().AsMilliseconds();
                
                if (sleep_time >= 0)
                {
                    Thread.Sleep(sleep_time);

                }
                clock.Restart();
            }
        }
    }
}
