using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drofsnar
{
    class Pacman
    {
        // Inner walls
        private List<int> wallPositions = new List<int>(){
            13,29,30,31,32,34,35,36,37,38,40,42,43,44,45,46,48,49,50,51,56,57,58,
            59,61,62,63,64,65,67,69,70,71,72,73,75,76,77,78,110,111,112,113,115,
            117,118,119,120,121,122,123,124,125,127,129,130,131,132,142,148,154,
            169,170,171,172,173,175,177,178,179,180,181,196,208,223,235,277,289,
            304,316,331,333,334,335,336,337,338,339,340,341,343,364,380,381,382,
            383,385,386,387,388,389,391,393,394,395,396,397,399,400,401,402,410,
            426,433,434,435,437,439,441,442,443,444,445,446,447,448,449,451,453,
            455,456,457,466,472,478,488,489,490,491,492,493,494,495,496,497,499,
            501,502,503,504,505,506,507,508,509,510
        };
        private Dictionary<int, char> specialChars = new Dictionary<int, char>() {
            { 162, '╚' },{ 163, '═' },{ 164, '═' },{ 165, '═' },{ 166, '═' },
            { 167, '╗' },{ 183, '╔' },{ 184, '═' },{ 185, '═' },{ 186, '═' },
            { 187, '═' },{ 188, '╝' },{ 194, '║' },{ 210, '║' },{ 216, '═' },
            { 217, '═' },{ 218, '═' },{ 219, '═' },{ 220, '═' },{ 221, '╝' },
            { 225, '╔' },{ 226, '═' },{ 227, '═' },{ 228, '═' },{ 230, '═' },
            { 231, '═' },{ 232, '═' },{ 233, '╗' },{ 237, '╚' },{ 238, '═' },
            { 239, '═' },{ 240, '═' },{ 241, '═' },{ 242, '═' },{ 252, '║' },
            { 253, ' ' },{ 254, ' ' },{ 258, ' ' },{ 259, ' ' },{ 260, '║' },
            { 270, '═' },{ 271, '═' },{ 272, '═' },{ 273, '═' },{ 274, '═' },
            { 275, '╗' },{ 279, '╚' },{ 280, '═' },{ 281, '═' },{ 282, '═' },
            { 283, '═' },{ 284, '═' },{ 285, '═' },{ 286, '═' },{ 287, '╝' },
            { 291, '╔' },{ 292, '═' },{ 293, '═' },{ 294, '═' },{ 295, '═' },
            { 296, '═' },{ 302, '║' },{ 318, '║' },{ 324, '╔' },{ 325, '═' },
            { 326, '═' },{ 327, '═' },{ 328, '═' },{ 329, '╝' },{ 345, '╚' },
            { 346, '═' },{ 347, '═' },{ 348, '═' },{ 349, '═' },{ 350, '╗' },
        };
        private List<int> visitedPositions = new List<int>();
        private List<int> bigDots = new List<int>(){
            55,79,406,430
        };
        private bool isAlive = true;
        private bool isScaryMode = false;
        private Random rand = new Random();

        public void Run()
        {
            int time = 0;
            List<Sprite> sprites = new List<Sprite>()
            {
                new Sprite("Pacman", ConsoleColor.Yellow, 13, 15, 0),
                new Sprite("Inky", ConsoleColor.Cyan, 12, 9, 8),
                new Sprite("Pinky", ConsoleColor.Magenta, 13, 9, 4),
                new Sprite("Blinky", ConsoleColor.Red, 13, 7, 0),
                new Sprite("Clyde", ConsoleColor.DarkYellow, 14, 9, 12),
            };

            while (isAlive)
            {
                DrawScreen(sprites, time);
                // Pacman's path
                movePacman(sprites[0], time);
                for (int s=1;s<sprites.Count;s++)
                {
                    if (time > sprites[s].waitTime)
                    {
                        moveInky(sprites[s], sprites[0]);
                    }
                }

                if (time > 150)
                {
                    isAlive = false;
                }
                time++;
            }
        }

        private void DrawScreen(List<Sprite> sprites, int time)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{" ",-5}{"1UP", -10}{"HIGH SCORE", -10}");
            Console.WriteLine($"{" ",-5}{"00",-10}{"00",-10}");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write('╔');
            Console.Write(new String('═', 25));
            Console.Write("╗\n");
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 27; x++)
                {
                    DrawPosition(sprites, x, y, time);
                    
                }
            }
            Console.Write('╚');
            Console.Write(new String('═', 25));
            Console.Write("╝\n");

            /*
            try
            {
                Console.WriteLine("Please enter your name within the next 5 seconds.");
                string name = Reader.ReadLine(20);
                Console.WriteLine("Hello, {0}!", name);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Sorry, you waited too long.");
            }
            */

            Thread.Sleep(20);
        }

        private void DrawPosition(List<Sprite> sprites, int x, int y, int time)
        {
            bool drewSprite = false;
            int position = getPosition(x, y);
            if (specialChars.ContainsKey(position))
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(specialChars[position]);
                if (x == 26)
                {
                    Console.Write('\n');
                }
            }
            else if (x == 0 && ((y < 6) || (y > 12)))
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write('║');
            }
            else if (x == 26)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                if ((y < 6) || (y > 12))
                {
                    Console.Write('║');
                }
                Console.Write('\n');
            }
            else
            {
                for (int s = 0; s < sprites.Count; s++)
                {
                    Sprite sprite = sprites[s];
                    if ((sprite.x == x) && (sprite.y == y))
                    {
                        Console.ForegroundColor = sprite.color;
                        if (sprite.name == "Pacman" && ((time % 2) == 0))
                        {
                            Console.Write('O');
                        }
                        else if (sprite.name == "Pacman" && ((time % 2) == 1))
                        {
                            Console.Write('C');
                        }
                        else if (!drewSprite)
                        {
                            Console.Write('Ω');
                        }

                        if (sprite.name == "Pacman")
                        {
                            visitedPositions.Add(position);
                        }
                        drewSprite = true;
                    }
                }
                if (!drewSprite)
                {
                    if (bigDots.Contains(position))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write('o');
                    }
                    else if (wallPositions.Contains(position))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write('█');
                    }
                    else if (!visitedPositions.Contains(position)
                        && ((y < 6) || (y > 12) || (x == 6) || (x == 20)))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write('·');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
            }
        }

        private int getPosition(int x, int y)
        {
            return 27 * y + x;
        }

        private void movePacman(Sprite Pacman, int time)
        {
            if (time < 7)
            {
                Pacman.MoveRight();
            }
            else if (time < 17)
            {
                Pacman.MoveUp();
            }
            else if (time < 22)
            {
                Pacman.MoveRight();
            }
            else if (time < 27)
            {
                Pacman.MoveUp();
            }
            else if (time < 38)
            {
                Pacman.MoveLeft();
            }
            else if (time < 41)
            {
                Pacman.MoveDown();
            }
            else if (time < 54)
            {
                Pacman.MoveLeft();
            }
            else if (time < 57)
            {
                Pacman.MoveUp();
            }
            else if (time < 62)
            {
                Pacman.MoveRight();
            }
            else if (time < 79)
            {
                Pacman.MoveDown();
            }
            else if (time < 84)
            {
                Pacman.MoveLeft();
            }
            else if (time < 86)
            {
                Pacman.MoveDown();
            }
            else if (time < 110)
            {
                Pacman.MoveRight();
            }
            else if (time < 112)
            {
                Pacman.MoveUp();
            }
            else if (time < 115)
            {
                Pacman.MoveLeft();
            }
            else if (time < 117)
            {
                Pacman.MoveUp();
            }
            else if (time < 120)
            {
                Pacman.MoveRight();
            }
            else if (time < 122)
            {
                Pacman.MoveUp();
            }
            else if (time < 133)
            {
                Pacman.MoveLeft();
            }
            else if (time < 135)
            {
                Pacman.MoveDown();
            }
            else if (time < 143)
            {
                Pacman.MoveLeft();
            }
            else if (time < 145)
            {
                Pacman.MoveUp();
            }
            else if (time < 150)
            {
                Pacman.MoveRight();
            }
        }

        private void moveInky(Sprite Inky, Sprite Pacman)
        {
            Dictionary<string, int> moves = new Dictionary<string, int>()
            {
                { "up", getPosition(Inky.x, Inky.y - 1) }, // UP
                { "dn", getPosition(Inky.x, Inky.y + 1) }, // DOWN
                { "lf", getPosition(Inky.x - 1, Inky.y) }, // LEFT
                { "rt", getPosition(Inky.x + 1, Inky.y) }, // RIGHT
            };
            List<string> validMoveIds = new List<string>();

            foreach (string key in moves.Keys.ToList())
            {
                int move = moves[key];
                if (!wallPositions.Contains(move)
                    && !specialChars.Keys.ToList().Contains(move)
                    && move != Inky.lastPosition)
                {
                    validMoveIds.Add(key);
                }
            }

            if (validMoveIds.Count > 0)
            {
                // Select a move
                int m = rand.Next(0, validMoveIds.Count);
                string moveId = validMoveIds[m];
                if (moveId == "up")
                {
                    // UP
                    Inky.MoveUp();

                } else if (moveId == "dn")
                {
                    // DOWN
                    Inky.MoveDown();

                } else if (moveId == "lf")
                {
                    // LEFT
                    Inky.MoveLeft();

                } else if (moveId == "rt")
                {
                    // RIGHT
                    Inky.MoveRight();

                }

                Inky.lastPosition = getPosition(Inky.x, Inky.y);
            }
        }
    }
}
