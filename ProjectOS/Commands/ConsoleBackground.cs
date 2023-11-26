using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System.Graphics;

namespace ProjectOS.Commands
{
    internal class ConsoleBackground : Command
    {
        public ConsoleBackground (String name) : base(name)
        {

        }

        public override string execute(string[] args)
        {
            String response;

            switch(args[0])
            {
                case "black":
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;

                // Setting console background color to blue
                case "blue":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;

                // Setting console PracticeOS color to cyan
                case "cyan":
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;

                // Setting console PracticeOS color to dark blue
                case "darkblue":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    break;

                // Setting console PracticeOS color to dark cyan
                case "darkcyan":
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;

                // Setting console background color to dark gray
                case "darkgray":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;

                // Setting console foreground color to dark green
                case "darkgreen":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;

                // Setting console background color to dark magenta
                case "darkmagenta":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    break;

                // Setting console background color to dark red
                case "darkred":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;

                // Setting console background color to dark yellow
                case "darkyellow":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;

                // Setting console background color to gray
                case "gray":
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;

                // Setting console background color to green
                case "green":
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;

                // Setting console background color to magenta
                case "magenta":
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;

                // Setting console background color to red
                case "red":
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;

                // Setting console background color to white
                case "white":
                    Console.BackgroundColor = ConsoleColor.White;
                    break;

                // Setting console background color to yellow 
                case "yellow":
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;

                // Handling the case when the provided color is not recognized
                default:
                    return response = "Invalid color";

            }

            return "";
        }
    }
}
