using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Drofsnar
{
    class Game
    {
        private static readonly int lifeCost = 10000;
        // private List<string> _events;
        private string[] _events;
        private static readonly Dictionary<string, int> scores = new Dictionary<string, int>
        {
            { "Bird", 10 },
            { "CrestedIbis", 100 },
            { "GreatKiskudee", 300 },
            { "RedCrossbill", 500 },
            { "Red-neckedPhalarope", 700 },
            { "EveningGrosbeak", 1000 },
            { "GreaterPrairieChicken", 2000 },
            { "IcelandGull", 3000 },
            { "Orange-belliedParrot", 5000 }
        };
        private static readonly Dictionary<string, string> properNames = new Dictionary<string, string>
        {
            { "Bird", "Bird" },
            { "CrestedIbis", "Crested Ibis" },
            { "GreatKiskudee", "Great Kiskudee" },
            { "RedCrossbill", "Red Crossbill" },
            { "Red-neckedPhalarope", "Red-Necked Phalarope" },
            { "EveningGrosbeak", "Evening Grosbeak" },
            { "GreaterPrairieChicken", "Greater Prairie Chicken" },
            { "IcelandGull", "Iceland Gull" },
            { "Orange-belliedParrot", "Orange-Bellied Parrot" },
            { "InvincibleBirdHunter", "Invincible Bird Hunter" },
            { "VulnerableBirdHunter", "Vulnerable Bird Hunter" }
        };

        public void SetEvents(string[] events)
        {
            _events = events;
        }

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            int score = 5000;
            int hunters = 0;
            int lives = 3;
            int spentScore = 0;
            string lifeEvent;
            bool dead = false;
            bool hasEarnedALife = false;

            if (_events.Length == 0)
            {
                Console.WriteLine("No events given.");
                return;
            }

            Console.WriteLine($"{"Event",-30} {"Score",-15} {"Lives",-15} {"Life Event",-15}");
            Console.WriteLine(new String('-', 75));

            foreach (string gameEvent in _events)
            {
                Console.ForegroundColor = ConsoleColor.White;
                lifeEvent = "";
                // Hunter or bird?
                if (gameEvent == "InvincibleBirdHunter")
                {
                    lives--;
                    lifeEvent = "-1 Life";
                    Console.ForegroundColor = ConsoleColor.Red;
                    hunters = 0;
                }
                else if (gameEvent == "VulnerableBirdHunter")
                {
                    double power = Math.Pow((double)2, (double)hunters);
                    score += 200 * Convert.ToInt32(power);
                    hunters++;
                    // One-line version of this part:
                    // score += Convert.ToInt32(200 * Math.Pow((double) 2, (double) hunters++));
                }
                else
                {
                    score += scores[gameEvent];
                }

                // Earn a life?
                if (score > 10000 && !hasEarnedALife)
                {
                    lives++;
                    spentScore += lifeCost;
                    lifeEvent = "+1 Life";
                    Console.ForegroundColor = ConsoleColor.Green;
                    hasEarnedALife = true;
                }

                // Dead?
                if (lives == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    dead = true;
                }

                // Output status
                Console.WriteLine($"{properNames[gameEvent], -30} {score, -15} {lives,-15} {lifeEvent,-15}");
                if (dead)
                {
                    Console.WriteLine($"\n~~~ YOU DIED! ~~~\n");
                    break;
                }
                else
                {
                    Thread.Sleep(50);
                }
            }

            // Output final score
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{"Final Score:",-30} {score,-15} {lives,-15}");
        }
    }
}
