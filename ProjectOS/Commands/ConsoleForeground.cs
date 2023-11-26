using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOS.Commands
{
    internal class ConsoleForeground : Command
    {
        public ConsoleForeground( String name) : base(name)
        {

        }

        public override String execute (String[] args)
        {
            String response;

            // Variable to store the response message
            switch (args[0])
            {
                // Setting console background color to black
                case "black":
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;

                // Setting console background color to blue
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;

                // Setting console background color to cyan
                case "cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;

                // Setting console background color to dark blue
                case "darkblue":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;

                // Setting console background color to dark cyan
                case "darkcyan":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;

                // Setting console background color to dark gray
                case "darkgray":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;

                // Setting console background color to dark green
                case "darkgreen":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;

                // Setting console background color to dark magenta
                case "darkmagenta":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;

                // Setting console background color to dark red
                case "darkred":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;

                // Setting console background color to dark yellow
                case "darkyellow":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;

                // Setting console background color to gray
                case "gray":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;

                // Setting console background color to green
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                // Setting console background color to magenta
                case "magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;

                // Setting console background color to red
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                // Setting console background color to white
                case "white":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                // Setting console background color to yellow 
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                // Handling the case when the provided color is not recognized
                default:
                    return response = "Invalid color";
            }
            // Constructing the response message
            response = "Console background color changed to: " + args[0];

            // Returning the response message
            return response;
        }
    }
}
