using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Drofsnar
{
    class Program
    {
        static void Main(string[] args)
        {
            // DungeonGame1 game = new DungeonGame1();

            // NewGame game = new NewGame();

            // Pacman game = new Pacman();

            Game game = new Game();
            game.SetEvents(GetEventsFromFile("C:/Users/andre/source/repos/Drofsnar/Drofsnar/game-sequence.txt"));
            game.Run();
        }

        // private static List<string> GetEventsFromFile(string path)
        private static string[] GetEventsFromFile(string path)
        {
            string text = File.ReadAllText("C:/Users/andre/source/repos/Drofsnar/Drofsnar/game-sequence.txt");
            // return text.Split(',').ToList();
            return text.Split(',');
        }
    }
}
