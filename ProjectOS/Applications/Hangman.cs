using Cosmos.System;
using ProjectOS.Commands;
using System;

namespace ProjectOS.Applications
{
    internal class Hangman : Command
    {
        private readonly string[] words = { "cosmos:noun", "hangman:noun", "programming:verb", "challenge:noun", "developer:noun", "RamOS:noun" };
        private string secretWord;
        private string wordType;
        private char[] guessedWord;
        private int maxGuesses = 6; // Number of parts in Hangman

        public Hangman(string name) : base(name) { }

        public override string execute(string[] args)
        {
            System.Console.Clear();
            StartGame();
            return " ";
        }

        private void StartGame()
        {
            System.Console.WriteLine("==========================");
            System.Console.WriteLine("||   \tHangman Game  \t||");
            System.Console.WriteLine("==========================");

            // Select a random word from the list
            string wordInfo = words[new Random().Next(words.Length)];
            string[] wordDetails = wordInfo.Split(':');
            secretWord = wordDetails[0];
            wordType = wordDetails[1];

            guessedWord = new char[secretWord.Length];

            // Initialize guessedWord with underscores
            for (int i = 0; i < guessedWord.Length; i++)
            {
                guessedWord[i] = '_';
            }

            int remainingGuesses = maxGuesses; // Declare remainingGuesses here

            while (remainingGuesses > 0)
            {
                DisplayHangman(remainingGuesses);  // Pass remainingGuesses as an argument
                DisplayGuessedWord();
                System.Console.WriteLine("Word Type: " + wordType);
                System.Console.WriteLine("Remaining Guesses: " + remainingGuesses);

                System.Console.Write("Enter a letter: ");
                char guess = System.Console.ReadLine().ToLower()[0];

                if (CheckGuess(guess))
                {
                    System.Console.WriteLine("Correct!");
                }
                else
                {
                    System.Console.WriteLine("Incorrect!");
                    remainingGuesses--;

                    if (remainingGuesses == 0)
                    {
                        System.Console.WriteLine("Game Over! The word was: " + secretWord);
                        break;
                    }
                }

                if (string.Join("", guessedWord) == secretWord)
                {
                    System.Console.WriteLine("Congratulations! You guessed the word: " + secretWord);
                    break;
                }
            }

            System.Console.WriteLine("Type \"1\" to Retry/Continue or type \"2\" to Exit Hangman");
            string user_input = System.Console.ReadLine();

            if (!string.IsNullOrEmpty(user_input))
            {
                if (user_input == "1")
                {
                    System.Console.Clear();
                    StartGame();
                }
                else if (user_input == "2")
                {
                    System.Console.Clear();
                    System.Console.WriteLine("Welcome to ProjectOS\nType \"help\" for a list of commands");
                }
            }
            else
            {
                System.Console.WriteLine("Error!");
            }
        }

        private void DisplayHangman(int remainingGuesses)
        {
            int incorrectGuesses = maxGuesses - remainingGuesses;

            System.Console.WriteLine("Hangman:");
            System.Console.WriteLine(" ____");

            if (incorrectGuesses >= 1)
                System.Console.WriteLine(" |   |");
            else
                System.Console.WriteLine(" |");

            if (incorrectGuesses >= 2)
                System.Console.WriteLine(" |   O");
            else
                System.Console.WriteLine(" |");

            if (incorrectGuesses >= 4)
                System.Console.Write(" |  /");
            else if (incorrectGuesses >= 3)
                System.Console.Write(" |   ");
            else
                System.Console.Write(" |");

            if (incorrectGuesses >= 3)
                System.Console.WriteLine("|");
            else
                System.Console.WriteLine();

            if (incorrectGuesses >= 6)
                System.Console.WriteLine(" |  / \\");
            else if (incorrectGuesses >= 5)
                System.Console.WriteLine(" |  /");
            else
                System.Console.WriteLine(" |");

            System.Console.WriteLine(" |");
        }

        private void DisplayGuessedWord()
        {
            System.Console.Write("Word: ");
            foreach (char letter in guessedWord)
            {
                System.Console.Write(letter + " ");
            }
            System.Console.WriteLine();
        }

        private bool CheckGuess(char guess)
        {
            bool correctGuess = false;

            for (int i = 0; i < secretWord.Length; i++)
            {
                if (secretWord[i] == guess && guessedWord[i] == '_')
                {
                    guessedWord[i] = guess;
                    correctGuess = true;
                }
            }

            return correctGuess;
        }
    }
}
