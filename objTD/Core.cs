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
    //Core component of the game, Game loop resides here aswell as the window and game initialization,
    // it shall be moved into startup manager or smth...


    class Core
    {
        static void OnClose(object sender, EventArgs e)
        {
            RenderWindow okno = (RenderWindow)sender;
            okno.Close();
        }
        static void OnClick(object sender, EventArgs e)
        {
            RenderWindow okno = (RenderWindow)sender;
            Console.WriteLine(Mouse.GetPosition(okno).ToString());
        }


        static void Main(string[] args)
        {
            const int PanelWidth = 64;
            int sleep_time = 0;
            const int FPS = 50;
            const int WindowWidth = 1280;
            const int WindowHeight = 960;
            const int TileSize = 64;
            const int SKIP_TICKS = 1000 / FPS;
           
            Clock globalclock = new Clock();
            Clock clock = new Clock();
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 8;
            RenderWindow okno = new RenderWindow(new VideoMode(WindowWidth +PanelWidth, WindowHeight), "okno", Styles.Close, settings);




            //map editor
            int[][] map = new int[(WindowWidth / TileSize)][];
            for (int i = 0; i < WindowWidth / TileSize; i++)
            {
                map[i] = new int[(WindowHeight / TileSize)];
            }
            for (int i = 0; i < WindowWidth / TileSize; i++)
            {
                map[i][4] = 1;
            }



            Classes.Game game = new Classes.Game(WindowWidth,WindowHeight,TileSize,map,PanelWidth);
            

            //event handles

            okno.Closed += new EventHandler(OnClose);



            int count = 0;
            clock.Restart();
            while (true)
            {
                //Console.WriteLine("{0} {1}",count++,clock.ElapsedTime.AsMilliseconds());
                okno.DispatchEvents();
                okno.Clear();
                game.Update(okno);

                game.Draw(okno);
                //Console.WriteLine(clock.ElapsedTime.AsMilliseconds());

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
