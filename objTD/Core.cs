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


    public enum EnemyPoints { Walkable, Unwalkable, UpDown, Right }

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
            const int TileSize = 32;
            const int SKIP_TICKS = 1000 / FPS;
            const int GridTileSizeX = WindowWidth / TileSize;
            const int GridTileSizeY = WindowHeight / TileSize;


            Clock globalclock = new Clock();
            Clock clock = new Clock();
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 8;
            RenderWindow okno = new RenderWindow(new VideoMode(WindowWidth +PanelWidth, WindowHeight), "okno", Styles.Close, settings);





            //Level editor

            //TODO REDO MAP EDITOR

            

            //event handles

            okno.Closed += new EventHandler(OnClose);

            Classes.PathFinding.PathGrid mapa = new Classes.PathFinding.PathGrid(WindowWidth, WindowHeight, TileSize);
            Classes.Game game = new Classes.Game(WindowWidth, WindowHeight, TileSize, mapa, PanelWidth);


            clock.Restart();
            while (true)
            {
                while(true)
                {
                    //CheckPause();
                    //add pause and main menu state
                    okno.DispatchEvents();
                    okno.Clear();

                    //Console.WriteLine("{0} start",clock.ElapsedTime.AsMilliseconds());
                    game.Update(okno);
                    game.Draw(okno);





                    okno.Display();


                    //Console.WriteLine("{0} stoped",clock.ElapsedTime.AsMilliseconds());


                    sleep_time = SKIP_TICKS - clock.Restart().AsMilliseconds();
                    //Console.WriteLine(sleep_time);
                    if (sleep_time >= 0)
                    {
                        //test and
                        //maybe try spinlock?
                        Thread.Sleep(sleep_time);

                    }
                    clock.Restart();

                }
            }
        }
    }
}
