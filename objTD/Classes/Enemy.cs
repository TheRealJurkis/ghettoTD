using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;
namespace objTD.Classes
{
    //Enemies are drawn and updated
    //TODO: redo constructor and add variability

    class Enemy
    {


        private int Health;
        private Vector2f Velocity;
        
        public CircleShape Manifestation { get; set; }

        public Enemy(int health)
        {
            Velocity = new Vector2f(1.5f,0);
            Health = health;

           Manifestation = new CircleShape();
           GiveStartTile(new Tile(0, 4, 64));
           Manifestation.FillColor = Color.Red;
           Manifestation.Radius = 16;



        }
        public void GiveStartTile(Tile starttile)
        {
            Manifestation.Position = new Vector2f(starttile.Tvar.Position.X, starttile.Tvar.Position.Y);
        }

        public void Update()
        {
           
            Manifestation.Position += Velocity;
            
            if(false)
            {
                //die
            }
        }

        public void Draw(RenderWindow okno)
        {
            if(true) //alive
            {
                okno.Draw(Manifestation);
            }
        }

    }
}
