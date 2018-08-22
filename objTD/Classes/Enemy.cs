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

        private static int EnemyCounter;
        public readonly int EnemyId;

        private int Health;
        private Vector2f Velocity;
        public CircleShape Manifestation { get; set; }
        public bool Dead { get; private set; }

        public Enemy(int health)
        {
           EnemyId = EnemyCounter++;
           Velocity = new Vector2f(1.5f,0);
           Health = health;
           Manifestation = new CircleShape();
           GiveStartTile(new Tile(0, 7, 64));
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
                Console.WriteLine("DEDED");
                Dead = true;
                //Somehow you need money from here..
            }
        }
        public virtual void Move()
        {

            Manifestation.Position += Velocity;

        }

        public void Update()
        {
            int x = (int)Manifestation.Position.X/64;
            int y = (int)Manifestation.Position.Y / 64;
            if (x == 2)
            {
                Velocity = new Vector2f(1, 2);
            }
            Move();
        }

        public void Draw(RenderWindow okno)
        {
            okno.Draw(Manifestation);
        }
    }
}
