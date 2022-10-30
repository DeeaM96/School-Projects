using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TRON.Game;

namespace TRON
{

    public class Player
    {
        public int X0 { get; set; }
        public int Y0 { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        /*public int Vx { get; set; }
        public int Vy { get; set; }*/
        public bool Alive { get; set; }
        public Color Color { get; set; }
        public int Points { get; set; }
        public bool Collision { get; set; }
        public DIRECTION X_Direction { get; set; }
        public DIRECTION Y_Direction { get; set; }


        public Player(Color Color)
        {
            this.Color = Color;

            Collision = false;
            Alive = false;
        }

        private DIRECTION GetOppositeDirectionX()
        {
            if (X_Direction == DIRECTION.LEFT) return DIRECTION.RIGHT;
            else if (X_Direction == DIRECTION.RIGHT) return DIRECTION.LEFT;
            else return DIRECTION.NULL;
        }

        private DIRECTION GetOppositeDirectionY()
        {
            if (Y_Direction == DIRECTION.UP) return DIRECTION.DOWN;
            else if (Y_Direction == DIRECTION.DOWN) return DIRECTION.UP;
            else return DIRECTION.NULL;
        }

        internal void UpdateDirection(DIRECTION x, DIRECTION y)
        {
            if (x == DIRECTION.NULL || x != GetOppositeDirectionX()) X_Direction = x;
            if (y == DIRECTION.NULL || y != GetOppositeDirectionY()) Y_Direction = y;
        }
    }
}
