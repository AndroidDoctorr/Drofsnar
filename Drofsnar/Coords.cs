using System;
using System.Collections.Generic;
using System.Text;

namespace Drofsnar
{
    public struct Coords
    {
        public int x, y;

        public Coords(int p1, int p2)
        {
            x = p1;
            y = p2;
        }

        public void moveRight()
        {
            x += 1;
        }

        public void moveLeft()
        {
            x -= 1;
        }
    }
}
