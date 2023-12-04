using ProjectOS.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Wordle 
    "5LetterDict.txt"
    - Generated from Donald Knuth at Stanford University.
    (https://homepage.cs.uiowa.edu/~sriram/21/fall04/words.html)
*/

namespace ProjectOS.Applications
{
    internal class Wordle
    {
        enum Color { gray, yellow, green }
        enum MessageType { error, info, success }

        static void Main(string[] args)
        {
            int tries, letter, currentTry;
            string[] words;
            string answer;

            void init(int Tries, int Letter)
            {
                tries = Tries;
                letter = Letter;
                currentTry = 0;
                words = data.AllWordsFromFile("5LetterDict.txt");
                answer = SetAnswer(words);
            }

            init(6, 5);
            bool correct = false;

            while (!correct && currentTry < tries)
            {
                printMessage($"Enter a {letter}-letter word. Current try is: {currentTry}", MessageType.info);
                string input = Console.ReadLine().ToLower();

                if (input.Length != 5)  // Check for 5-letter words
                {
                    printMessage("This is not a valid 5-letter word.", MessageType.error);
                }
                else if (words.Contains(input))
                {
                    correct = CheckWord(input, answer, letter);
                    currentTry++;
                }
                else
                {
                    printMessage("Word is not in the list.", MessageType.error);
                }
            }

            if (!correct)
            {
                printMessage("Sorry, You Failed. The Word was:", MessageType.info);
                printMessage(answer, MessageType.success);
            }
        }

        static string SetAnswer(string[] wordList)
        {
            Random rnd = new Random();
            int r = rnd.Next(0, wordList.Length);
            return wordList[r];
        }

        static bool CheckWord(string input, string a, int count)
        {
            if (input == a)
            {
                printMessage(input, MessageType.success);
                printMessage("Congratulations! You Won.", MessageType.success);
                return true;
            }

            char[] answer = a.ToCharArray();
            char[] inputArray = input.ToCharArray();
            Color color;

            for (int i = 0; i < count; i++)
            {
                color = Color.gray;

                for (int d = 0; d < answer.Length; d++)
                {
                    if (inputArray[i] == answer[d])
                    {
                        color = d == i ? Color.green : Color.yellow;
                        if (color == Color.green)
                        {
                            answer[d] = ' ';
                            break;
                        }
                    }
                }

                printLetter(color, inputArray[i]);
            }

            Console.WriteLine("");
            return false;
        }

        static void printLetter(Color color, char letter)
        {
            switch (color)
            {
                case Color.green:
                    printText(letter.ToString(), ConsoleColor.Green, ConsoleColor.White, false);
                    break;
                case Color.yellow:
                    printText(letter.ToString(), ConsoleColor.Yellow, ConsoleColor.Black, false);
                    break;
                case Color.gray:
                    printText(letter.ToString(), ConsoleColor.Gray, ConsoleColor.Black, false);
                    break;
            }
        }

        static void printText(string text, ConsoleColor bColor, ConsoleColor fColor, bool isLine)
        {
            Console.BackgroundColor = bColor;
            Console.ForegroundColor = fColor;
            if (isLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }
            Console.ResetColor();
        }

        static void printMessage(string message, MessageType type)
        {
            switch (type)
            {
                case MessageType.error:
                    printText(message, ConsoleColor.Red, ConsoleColor.White, true);
                    break;
                case MessageType.info:
                    printText(message, ConsoleColor.White, ConsoleColor.Black, true);
                    break;
                case MessageType.success:
                    printText(message, ConsoleColor.Green, ConsoleColor.Black, true);
                    break;
            }
        }
    }

    public static class data
    {

        public static string[] AllWordsFromFile(string filePath)
        {
            try
            {
                return File.ReadAllLines(@"C:\\Users\\Windows 10\\source\\repos\\ProjectOS\\ProjectOS\\Applications\\5LetterDict.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading words from file: {ex.Message}");
                return new string[0]; // Return an empty array in case of an error.
            }
        }
    }
}

