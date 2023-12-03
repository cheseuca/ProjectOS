using Cosmos.System;
using ProjectOS.Commands;
using System;
using System.Collections.Generic;

namespace ProjectOS.Applications
{
    internal class NumberGuessingGame : Command
    {
        private int minNumber = 1;
        private int maxNumber = 100;
        private int secretNumber;
        private int attempts = 0;
        private List<int> userGuesses = new List<int>();

        public NumberGuessingGame(string name) : base(name) { }

        public override string execute(string[] args)
        {
            System.Console.Clear();
            StartGame();
            return " ";
        }
        // added comments here hehehehe 
        private void StartGame()
        {
            System.Console.WriteLine("==========================");
            System.Console.WriteLine("|| Number Guessing Game ||");
            System.Console.WriteLine("==========================");

            System.Console.WriteLine("Choose a difficulty level:");
            System.Console.WriteLine("1. Easy (1-50)");
            System.Console.WriteLine("2. Medium (1-100)");
            System.Console.WriteLine("3. Hard (1-200)");
            int difficultyChoice = GetDifficultyChoice();

            SetDifficulty(difficultyChoice);

            // Generate a random number between minNumber and maxNumber
            System.Random random = new System.Random();
            secretNumber = random.Next(minNumber, maxNumber + 1);

            System.Console.WriteLine($"I've selected a number between {minNumber} and {maxNumber}. Try to guess it!");

            int maxAttempts = 10; // Maximum number of attempts

            while (attempts < maxAttempts)
            {
                System.Console.Write("Enter your guess: ");
                string input = System.Console.ReadLine();

                if (int.TryParse(input, out int userGuess))
                {
                    attempts++;
                    userGuesses.Add(userGuess);

                    if (userGuess < minNumber || userGuess > maxNumber)
                    {
                        System.Console.WriteLine($"Please enter a number between {minNumber} and {maxNumber}.");
                    }
                    else if (userGuess < secretNumber)
                    {
                        System.Console.WriteLine("Too low! Try again.");
                        DisplayFeedback(userGuess);
                    }
                    else if (userGuess > secretNumber)
                    {
                        System.Console.WriteLine("Too high! Try again.");
                        DisplayFeedback(userGuess);
                    }
                    else
                    {
                        System.Console.WriteLine($"\nCongratulations! You guessed the number {secretNumber} in {attempts} attempts.");

                        // Score System
                        int score = CalculateScore();
                        System.Console.WriteLine($"Your score: {score}");

                        // Display user's guesses
                        System.Console.Write("Your guesses: ");
                        foreach (var guess in userGuesses)
                        {
                            System.Console.Write(guess + " ");
                        }
                        System.Console.WriteLine(); // Move to the next line after displaying guesses

                        // Ask the user if they want to retry or exit
                        System.Console.WriteLine("\n\nType \"1\" to Retry/Continue or type \"2\" to Exit");
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
                                return;
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("Error!");
                            return;
                        }
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            System.Console.WriteLine($"\nGame over! You've reached the maximum attempts. The secret number was {secretNumber}.");
            System.Console.WriteLine("Thanks for playing the Number Guessing Game!");
        }

        private void DisplayFeedback(int guess)
        {
            int difference = Math.Abs(secretNumber - guess);

            if (difference <= 10)
            {
                System.Console.WriteLine("Getting warmer!");
            }
            else if (difference <= 20)
            {
                System.Console.WriteLine("You're warm!");
            }
            else
            {
                System.Console.WriteLine("You're far off.");
            }
        }

        private void SetDifficulty(int difficultyChoice)
        {
            switch (difficultyChoice)
            {
                case 1:
                    maxNumber = 50;
                    break;
                case 2:
                    maxNumber = 100;
                    break;
                case 3:
                    maxNumber = 200;
                    break;
                default:
                    maxNumber = 100;
                    break;
            }
        }

        private int GetDifficultyChoice()
        {
            int choice;
            while (!int.TryParse(System.Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                System.Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                System.Console.WriteLine("Choose a difficulty level:");
                System.Console.WriteLine("1. Easy (1-50)");
                System.Console.WriteLine("2. Medium (1-100)");
                System.Console.WriteLine("3. Hard (1-200)");
            }
            return choice;
        }

        private int CalculateScore()
        {
            return 100 - (attempts * 5);
        }
    }
}
