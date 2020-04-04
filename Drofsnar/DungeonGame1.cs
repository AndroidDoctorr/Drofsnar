using System;
using System.Collections.Generic;
using System.Text;

namespace Drofsnar
{
    class DungeonGame1
    {
        private bool swordFound = false;
        public void Run()
        {
            // Initialize some stuff

            string[] lines = new string[]
            {
                "lives=3",
                "score=100"
            };

            System.IO.File.WriteAllLines(@"C:/Users/andre/source/repos/Drofsnar/Drofsnar/testfile.sav", lines);

            string text = System.IO.File.ReadAllText(@"C:/Users/andre/source/repos/Drofsnar/Drofsnar/testfile.sav");

            foreach (char letter in "something")
            {
                Random rand = new Random();
                int choice = rand.Next(0, 6);
                switch (choice)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
                Console.Write(letter + "blah\n");
            }
            

            // RoomA();
        }
        public void RoomA()
        {
            bool responseIsValid = false;
            while (!responseIsValid)
            {
                Console.Clear();
                Console.WriteLine("u r in dungeon, left, right, or up?");
                string response = Console.ReadLine();
                if (response == "left")
                {
                    roomB();
                    responseIsValid = true;
                }
                else if (response == "right")
                {
                    roomC();
                    responseIsValid = true;
                }
                else if (response == "up")
                {
                    Console.WriteLine("You can't go up!");
                }
            }
        }

        public void roomB()
        {
            Console.WriteLine("There's nothing here. Go back?");
            string response = Console.ReadLine();
            if (response == "back")
            {
                RoomA();
            }

        }

        public void roomC()
        {
            Console.WriteLine("You find a sword!");
            swordFound = true;
        }
    }
}
