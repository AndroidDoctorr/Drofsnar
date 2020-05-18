using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Drofsnar
{
    public class NewGame
    {
        public void Run()
        {
            string text = System.IO.File.ReadAllText(@"C:/Users/andre/source/repos/Drofsnar/Drofsnar/game-sequence.txt");

            



            // Console.WriteLine(text);
            string[] birds = text.Split(',');
            int score = 5000;
            int hunterScoreMultiplier = 1;
            int lives = 3;
            bool earnedALife = false;

            Dictionary<string, int> scores = new Dictionary<string, int>
            {
                { "Bird", 100 },
                { "CrestedIbis", 1000 },
                { "GreaterPrairieChicken", 2000 },
                { "Orange-belliedParrot", 5000 },
                { "EveningGrosbeak", 1000 },
            };

            foreach (string bird in birds)
            {
                Console.ForegroundColor = ConsoleColor.White;
                // Console.WriteLine(bird);
                if (scores.ContainsKey(bird))
                {
                    score += scores[bird];
                    // someList[0];
                } else if (bird == "VulnerableBirdHunter")
                {
                    score += 200 * hunterScoreMultiplier;
                    hunterScoreMultiplier *= 2;
                } else if (bird == "InvincibleBirdHunter")
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    lives--;
                }
                
                if (!earnedALife && score > 10000)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    lives++;
                    earnedALife = true;
                }

                Console.WriteLine($"{ bird, -30 } - { score, -10 } - {lives, -8}");
                Thread.Sleep(50);
                if (lives == 0)
                {
                    break;
                }

                
            }
            Console.WriteLine(score);
        }
    }
}
