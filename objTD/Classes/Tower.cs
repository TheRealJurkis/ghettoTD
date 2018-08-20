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

    //basic tower class, has tower related attributes,
    //TODO: tower type
    //shoots ,draws and updates

    class Tower
    {
        int Damage;
        int FireRateMS;
        int Cost;
        int i = 1;


        bool Reloading;

        RectangleShape tvar;
        RectangleShape kanon;
        public CircleShape AttackRadius;

        List<Projectiles> TowerProjectiles;

        Clock TowerClock;

        CircleShape Target;

        public Tower(int Xtile,int Ytile,int attradius)
        {
            TowerClock = new Clock();
         


            //tilesize is hardcoded consider refactoring

            kanon = new RectangleShape();
            AttackRadius = new CircleShape();
            tvar = new RectangleShape();
            TowerProjectiles = new List<Projectiles>();


            //tower
            tvar.Size = new Vector2f(64, 64);



            tvar.FillColor = Color.Cyan;
            tvar.Position = new Vector2f(Xtile*64,Ytile*64);


            AttackRadius.Position = tvar.Position;
            AttackRadius.Radius = attradius;
            AttackRadius.FillColor = new Color(45, 0, 0, 45);
            AttackRadius.OutlineColor = Color.Red;
            AttackRadius.OutlineThickness = 1f;
            AttackRadius.Position -= new Vector2f(attradius,attradius);
            AttackRadius.Position += tvar.Size / 2;




            kanon.Size = new Vector2f(64,8);
            kanon.Position = tvar.Position;
            kanon.FillColor = Color.Black;
            kanon.Origin = new Vector2f(32,4);
            kanon.Position += tvar.Size / 2;


           // ShootAt();
        }

        public void ShootAt(Vector2f v)
        {
            Projectiles p = new Projectiles(new Vector2f(100, 800), new Vector2f(1, 1));
            p.GiveTarget(v);
            TowerProjectiles.Add(p);

        }


        public void Update()
        {
            for (int i = TowerProjectiles.Count - 1; i >= 0; i--)
            {
                TowerProjectiles.ElementAt(i).Update();
            }

            if(TowerClock.ElapsedTime.AsSeconds() >= 0.5)
            {
                //ShootAt();
                TowerClock.Restart();
            }

        }

        public void Draw(RenderWindow okno)
        {
            okno.Draw(tvar);
            okno.Draw(AttackRadius);
            okno.Draw(kanon);



           
            for (int i = TowerProjectiles.Count - 1; i >= 0; i--)
            {
                TowerProjectiles.ElementAt(i).Draw(okno);
            }

        

        }
    }

}
