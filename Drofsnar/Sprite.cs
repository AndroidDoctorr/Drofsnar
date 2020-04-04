using System;
using System.Collections.Generic;
using System.Text;

namespace Drofsnar
{
    class Sprite
    {
        public string name { get; }
        public ConsoleColor color { get; }
        public int x;
        public int y;
        public int waitTime;
        public int lastPosition = 0;

        public Sprite(string spriteName, ConsoleColor spriteColor, int px, int py, int wait)
        {
            name = spriteName;
            color = spriteColor;
            x = px;
            y = py;
            waitTime = wait;
        }
        public Coords GetCoords()
        {
            return new Coords(x, y);
        }
        public void MoveLeft()
        {
            x -= 1;
        }
        public void MoveRight()
        {
            x += 1;
        }

        public void MoveUp()
        {
            y -= 1;
        }

        public void MoveDown()
        {
            y += 1;
        }
    }
}
