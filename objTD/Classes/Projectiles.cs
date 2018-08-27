using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using BasicVector;

namespace objTD.Classes
{

    /*Projectiles shot by a tower have dmg velocity and a target
    */



    class Projectiles
    {

        public CircleShape RigidBody;
        Enemy Target;
        int speed = 10;
        public bool Dead { get; set; }
        public int DMG { get; set; }
        public bool Hit { get; private set; }

        public Projectiles(Tower veza)
        {
            RigidBody = new CircleShape();
            RigidBody.FillColor = Color.Black;
            RigidBody.Radius = 4;
            RigidBody.Position = veza.AttackRadius.Position;
            DMG = veza.Damage;
            Dead = false;
        }

        public virtual void Update()
        {
            if (Target == null) { return; }
            if(RigidBody.GetGlobalBounds().Intersects(Target.Manifestation.GetGlobalBounds()))
            {
                Target.Hit(this);
                Die();
            }

            Vector2f smer = Target.Manifestation.Position - this.RigidBody.Position;
            Vector v = VectorUtil.Normalize(new Vector(smer.X, smer.Y));

            RigidBody.Position += new Vector2f((float)v.X,(float)v.Y) * speed;
        }

        public void GiveTarget(Enemy e)
        {
            Target = e;
        }

        private void Die()
        {
            Dead = true;
        }

        public void Draw(RenderWindow okno)
        {
            okno.Draw(RigidBody);
        }






    }
}
