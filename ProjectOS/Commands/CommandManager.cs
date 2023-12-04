using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectOS.Commands
{
    internal class CommandManager
    {
        private List<Command> commands;

        public CommandManager()
        {   
            // ignore this
            commands = new List<Command>(13);
            this.commands.Add(new Help("help"));
            this.commands.Add(new ShutDown("shutdown"));
            this.commands.Add(new Restart("restart"));
            this.commands.Add(new Clear("clear"));
            this.commands.Add(new getSysInfo("sysinfo"));
            this.commands.Add(new File("file"));
            this.commands.Add(new ConsoleForeground("foreground"));
            this.commands.Add(new ConsoleBackground("background"));
            this.commands.Add(new Applications.Calc("calc"));
            this.commands.Add(new Applications.Hangman("hangman"));
            this.commands.Add(new Applications.NumberGuessingGame("feelinglucky"));
            this.commands.Add(new Applications.WordleCommand("wordle"));
            // this.commands.Add(new Applications.TicTacToe("xoxo"));
            this.commands.Add(new DateTime ("datetime"));
        }

        public String processInput(String input)
        {
            Console.WriteLine("### Input received: '" + input + "'"); // Added this line for debugging

            String[] split = input.Split(' ');

            if (split.Length < 1)
            {
                return "Invalid input. Please provide a command.";
            }

            String commandName = split[0];

            List<String> args = split.Skip(1).ToList();

            foreach (Command cmd in this.commands)
            {
                if (cmd.name == commandName)
                    return cmd.execute(args.ToArray());
            }

            return "Your command \"" + commandName + "\" does not exist";
        }

    }
}
