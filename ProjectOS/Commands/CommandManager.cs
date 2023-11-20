using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOS.Commands{
    internal class CommandManager{
        private List<Command> commands;

        public CommandManager()
        {
            commands = new List<Command>(6);
            this.commands.Add(new Help("help"));
            this.commands.Add(new ShutDown("shutdown"));
            this.commands.Add(new Restart("restart"));
            this.commands.Add(new Version("version"));
            this.commands.Add(new Clear("clear"));
            this.commands.Add(new File("file"));
            this.commands.Add(new Directory("dir"));
            this.commands.Add(new ChangeDirectory("cd"));
            
        }

        public String processInput (String input) {
            String[] split = input.Split(' ');

            String commandName = split[0];

            List<String> args = new List<String>();

            int ctr = 0;
            foreach (String s in split)
            {
                if (ctr != 0)
                    args.Add(s);
                ++ctr;
            }

            foreach (Command cmd in this.commands)
            {
                if (cmd.name == commandName)
                    return cmd.execute(args.ToArray());
            }

            return "Your command \""+commandName+"\" does not exist";
        }
    }
}
