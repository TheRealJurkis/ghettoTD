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
    enum MyEnum
    {
        regular,thick,fast,flying
    }


    class Enemy
    {
        const int TileSize = 32;
        private static int EnemyCounter;
        public readonly int EnemyId;

        private int Health;
        public Location location { get; set; }
        public CircleShape Manifestation { get; set; }
        public bool Dead { get; private set; }
        private float speed = 2.5f;



        public Enemy(int health)
        {
           EnemyId = EnemyCounter++;
           Health = health;
           Manifestation = new CircleShape();
           Manifestation.FillColor = Color.Red;
           Manifestation.Radius = 16;
        }

        public void GiveStartTile(Tile starttile)
        {
            Manifestation.Position = new Vector2f(starttile.Tvar.Position.X, starttile.Tvar.Position.Y);
        }

        public void Hit(Projectiles p)
        {
            Health -= p.DMG;
            if(Health<=0)
            {
                Dead = true;
                //Somehow you need money from here..
            }
        }
        public virtual void Move(Vector2f v)
        {

            Manifestation.Position += v * speed;

        }

        public void Update()
        {
            int x = (int)Manifestation.Position.X / 32;
            int y = (int)Manifestation.Position.Y / 32;

            location = new Location(x, y);
        }

        public void Draw(RenderWindow okno)
        {
            okno.Draw(Manifestation);
        }
    }
}
