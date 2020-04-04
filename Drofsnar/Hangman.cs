using System;
using System.Collections.Generic;
using System.Text;

namespace Drofsnar
{
    class Hangman
    {
        public void Run()
        {
            int guessesLeft = 6;
            int letters = 5;
            string word = "chair";
            while (guessesLeft > 0)
            {
                Console.WriteLine("Guess a letter:");
                Console.WriteLine(new String('_', letters));
                char guess = Console.ReadKey().KeyChar;
                if (word.Contains(guess))
                {

                } else
                {
                    guessesLeft--;
                }
            }

        }
    }
}
