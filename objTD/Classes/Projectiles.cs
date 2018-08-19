using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace objTD.Classes
{
    class Projectiles
    {

        public RectangleShape RigidBody;
        Vector2f Velocity;
        Vector2DUtil vmule;
        Vector2f Target;

        public Projectiles(Vector2f pozicia, Vector2f velocity)
        {
            vmule = new Vector2DUtil();
            RigidBody = new RectangleShape();
            RigidBody.FillColor = Color.Cyan;
            RigidBody.Size = new Vector2f(12, 64);
            RigidBody.Position = pozicia;
            Velocity = new Vector2f(0, 0);
           
        }

        public void GiveTarget(Vector2f v)
        {
            Target = v;
        }

        public void Update()
        {
            Vector2f smer = Target - this.RigidBody.Position;

            Vector2f test = vmule.NormalizeVector(smer);

            RigidBody.Position += test;
        }

        public void Draw(RenderWindow okno)
        {
            okno.Draw(RigidBody);
        }

    }
}
