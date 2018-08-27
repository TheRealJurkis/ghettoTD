using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;
namespace objTD.Classes
{
    /*Enemies are drawn and updated here
    * TODO: add variability...
    */
    enum EnemyType
    {
        Regular=0,Thick,Fast
    }


    class Enemy
    {
        const int TileSize = 32;
        private static int EnemyCounter;
        public readonly int EnemyId;

        public int Health { get; private set; }
        public Location location { get; set; }
        public CircleShape Manifestation { get; set; }
        public bool Dead { get; private set; }
        private float speed;



        public Enemy(int level,Vector2f start,EnemyType e)
        {
            EnemyId = EnemyCounter++;
            Manifestation = new CircleShape();
            Manifestation.Position = start;
            Health = level;
            speed = 2f;

            switch (e)
            {
                case EnemyType.Fast:
                    Manifestation.FillColor = Color.Blue;
                    Manifestation.Radius = 6;
                    speed = 3f;
                    break;
                case EnemyType.Regular:
                    Manifestation.FillColor = Color.Magenta;
                    Manifestation.Radius = 6;
                    break;
                case EnemyType.Thick:
                    Manifestation.FillColor = Color.Red;
                    Manifestation.Radius = 6;
                    speed = 1f;
                    Health = Health * 20;
                    break;
            }
        }
  
        public void Hit(Projectiles p)
        {
            Health -= p.DMG;
            if(Health<=0)
            {
                Dead = true;
            }
        }

        //move in the direction Flowgrid dictates...
        public virtual void Move(Vector2f direction)
        {
            Manifestation.Position += direction * speed;
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
