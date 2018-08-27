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
        public RectangleShape Manifestation { get; set; }
        public EnemyType type;
        public bool Dead { get; private set; }
        private float speed;



        public Enemy(int level,Vector2f start,EnemyType e)
        {
            EnemyId = EnemyCounter++;
            Manifestation = new RectangleShape();
            Manifestation.Position = start;
            Health = level;
            speed = 2f;
            type = e;
            Manifestation.Size = new Vector2f(30, 30);
            Manifestation.Origin = Manifestation.Size / 2;

            switch (e)
            {
                case EnemyType.Fast:
                    speed = 3f;
                    Manifestation.Texture = new Texture("objTD/Assets/Textures/fast.png");
                    break;
                case EnemyType.Regular:
                    Manifestation.Texture = new Texture("objTD/Assets/Textures/regular.png");
                    break;
                case EnemyType.Thick:
                    speed = 1f;
                    Health = Health * 20;
                    Manifestation.Texture = new Texture("objTD/Assets/Textures/tank.png");
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

            if(direction.X == 0 && direction.Y==-1)
            {
                Manifestation.Rotation = 0;
            }
            if (direction.X == 0 && direction.Y == 1)
            {
                Manifestation.Rotation = 180;
            }
            if (direction.X == 1 && direction.Y == 0)
            {
                Manifestation.Rotation = 90;
            }
            if (direction.X == -1 && direction.Y == 0)
            {
                Manifestation.Rotation = 270;
            }
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
