using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace objTD.Classes
{
    class Game
    {
        //Game builds the map  TODO: make an editor,
        //updates and draws its components,
        // TODO: Improve naming and hardcoded...

        Tile StartTile;
        TileMap map;
        Player player;
        SidePanel sp;
        BattleComponent battlecomponent;

        //load from somekind of an initfile maybe?
        string Dirt = "dirt.jpg";
        string Grass = "grass.jpg";



        public Game(int width,int height,int tilesize,int[][] mapa,int panelwidth)
        {
            //mapa by sa asi mala stavat niekde inde
            map = new TileMap(width, height, tilesize);
            map.LoadTextures(Grass,Dirt);
            map.BuildMap(mapa);

            StartTile = map.GetTile(0, 4);


            player = new Player(width,height,tilesize,panelwidth);
            battlecomponent = new BattleComponent();
            battlecomponent.LoadMap(map);
            sp = new SidePanel(width,panelwidth,height,tilesize);
            
        }

        public void Update(RenderWindow okno)
        {
            player.Update(okno);
            battlecomponent.Update(player);
        }


        public void Draw(RenderWindow okno)
        {
            sp.Draw(okno);
            map.Draw(okno);
            player.Draw(okno);
            battlecomponent.Draw(okno);
        }
    }
}
