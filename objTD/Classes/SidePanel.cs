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
    class SidePanel
    {
        RectangleShape[] side;


        public SidePanel(int start,int width, int height,int sidetile)
        {
            side = new RectangleShape[height / sidetile];
            for (int i = 0; i < side.Length; i++)
            {
                side[i] = new RectangleShape();
            }
            side[0].FillColor = Color.Green;
            side[0].Position = new Vector2f(start, 0);
            side[0].Size = new Vector2f(sidetile, sidetile);
            side[1].FillColor = Color.Red;
            side[1].Position = new Vector2f(start, sidetile);
            side[1].Size = new Vector2f(sidetile, sidetile);
        }

        public void Update(RenderWindow okno)
        {

        }

        public void Draw(RenderWindow okno)
        {
            okno.Draw(side[0]);
            okno.Draw(side[1]);
        }
    }
}
