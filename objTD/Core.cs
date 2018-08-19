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
            const int FPS = 80;   
            const int WindowWidth = 1280 + PanelWidth;
            const int WindowHeight = 960;
            const int TileSize = 64;
            const int SKIP_TICKS = 1000 / FPS;

           
            Clock globalclock = new Clock();
            Clock clock = new Clock();
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 8;
            RenderWindow okno = new RenderWindow(new VideoMode(WindowWidth, WindowHeight), "okno", Styles.Close, settings);

            RenderWindow okno2 = new RenderWindow(new VideoMode(100, 800), "okno2", Styles.Close);



            //map editor
            int[][] map = new int[WindowWidth - PanelWidth / TileSize][];
            for (int i = 0; i < WindowWidth -PanelWidth / TileSize; i++)
            {
                map[i] = new int[WindowHeight -PanelWidth / TileSize];
            }
            for (int i = 0; i < WindowWidth - PanelWidth / TileSize; i++)
            {
                map[i][4] = 1;
            }



            Classes.Game game = new Classes.Game(WindowWidth -PanelWidth,WindowHeight,TileSize,map);
            Classes.SidePanel sidepanel = new Classes.SidePanel(PanelWidth, WindowHeight);


            //event handles

            okno.Closed += new EventHandler(OnClose);





            clock.Restart();
            while (true)
            {
                okno.DispatchEvents();
                okno.Clear();

                okno2.DispatchEvents();
                okno2.Clear();


                game.Update(okno);
                game.Draw(okno);

               
                okno.Display();
                okno2.Display();






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
