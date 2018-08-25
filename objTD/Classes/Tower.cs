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

    public enum TowerTypes
    {
        Homing,Laser,Sonic
    }



    class Tower
    {
        const int TileSize = 32;
        public int Damage { get; private set; }
        int FireRateMS = 500;
        int Cost;
        public CircleShape AttackRadius;


        public bool ReadyToShoot { get; private set; }

        Sprite kanon;

        List<Projectiles> TowerProjectiles;

        Clock TowerClock;

        public Tower(PathFinding.PathNode node,TowerTypes e)
        {
            TowerClock = new Clock();
            kanon = new Sprite();
            AttackRadius = new CircleShape();
            TowerProjectiles = new List<Projectiles>();

            //hardcoded tilesize..

            switch (e)
            {
                case TowerTypes.Homing:
                    
                    break;
                case TowerTypes.Laser:
                    kanon.Texture = new Texture("Laser3.png");
                    kanon.Position = new Vector2f(node.NodeLocation.x*TileSize, node.NodeLocation.y*TileSize) + new Vector2f(TileSize/2, TileSize/2);
                    kanon.Origin = new Vector2f(TileSize / 2, TileSize / 2);
                    AttackRadius.Radius = 200;
                    Damage = 10;
                    break;
                case TowerTypes.Sonic:

                    break;

            }
            //tower


            AttackRadius.Position = kanon.Position;
            AttackRadius.Origin = new Vector2f(200,200);
            AttackRadius.FillColor = new Color(0, 0, 0,1);
            AttackRadius.OutlineColor = Color.Red;
            AttackRadius.OutlineThickness = 1f;




            kanon.Position = AttackRadius.Position;
            kanon.Origin = new Vector2f(TileSize/2, TileSize/2);

        }

        public void ShootAt(Enemy e)
        {
            Projectiles p = new Projectiles(this);
            p.GiveTarget(e);
            TowerProjectiles.Add(p);
        }

        public void Rotate(Enemy e)
        {
            Vector2f pozicia = e.Manifestation.Position;

            float angle = (float)Math.Atan2(pozicia.Y - kanon.Position.Y, pozicia.X - kanon.Position.X);
            angle =(float)(angle * (180 / Math.PI));

            this.kanon.Rotation = 90 + angle;
        }

        public void Update()
        {
            //Updates positions of projectiles and removes any dead ones

            for (int i = TowerProjectiles.Count - 1; i >= 0; i--)
            {
                TowerProjectiles.ElementAt(i).Update();
                if(TowerProjectiles.ElementAt(i).Dead)
                {
                    TowerProjectiles.RemoveAt(i);
                }
            }

            if(TowerClock.ElapsedTime.AsMilliseconds() >= FireRateMS)
            {
                ReadyToShoot = true;
                TowerClock.Restart();
            }
            else { ReadyToShoot = false; }
        }

        public void Draw(RenderWindow okno)
        {
            okno.Draw(AttackRadius);
            okno.Draw(kanon);

            for (int i = TowerProjectiles.Count - 1; i >= 0; i--)
            {
                TowerProjectiles.ElementAt(i).Draw(okno);
            }
        }
    }
}
