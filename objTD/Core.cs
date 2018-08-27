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
     * maybe should be encapsulated further..
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




            const int PanelWidth = 0;

            int sleep_time = 0;
            const int FPS = 50;
            const int WindowWidth = 1280;
            const int WindowHeight = 960;
            const int TileSize = 32;
            const int SKIP_TICKS = 1000 / FPS;
            bool GamePaused = false;

            //game init stuff
            Clock Pauseclock = new Clock();
            Clock clock = new Clock();
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 8;
            RenderWindow okno = new RenderWindow(new VideoMode(WindowWidth +PanelWidth, WindowHeight), "okno", Styles.Close, settings);
            Classes.PathFinding.PathGrid mapa = new Classes.PathFinding.PathGrid(WindowWidth, WindowHeight, TileSize);
            Classes.Game game = new Classes.Game(WindowWidth, WindowHeight, TileSize, mapa, PanelWidth);

            //delay
            Console.WriteLine("Game starts in 5 seconds");
            while (clock.ElapsedTime.AsSeconds() <= 5)
            {
            }




            //events
            game.PauseGame += new Classes.Game.PauseHandler(Pause);
            okno.Closed += new EventHandler(OnClose);
            game.TurnOffGame += new Classes.Game.PauseHandler(GameOver);
            void Pause(Classes.Game g, EventArgs e)
            {
                if (Pauseclock.ElapsedTime.AsMilliseconds() > 500)
                    GamePaused = true;
                Pauseclock.Restart();
            }
            void GameOver(Classes.Game g, EventArgs e)
            {
                okno.Close();
                GamePaused = true;
            }



            //Main Game loop


            clock.Restart();
            while (true)
            {
                while(GamePaused)
                {
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && Pauseclock.ElapsedTime.AsMilliseconds() >100)
                    {
                        GamePaused = false;
                        Pauseclock.Restart();
                    }
                }

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
