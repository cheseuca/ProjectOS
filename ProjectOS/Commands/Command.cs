using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOS.Commands{
    internal class Command{
        
        public readonly string name;
        
        public Command(string name)
        {
            this.name = name;
        }

        public virtual String execute(String[] args)
        {
            return "Command not found";
        }
    }

    internal class Help : Command
    {

        public Help(String name) : base(name) { }

        public override String execute(String[] args)
        {
            return @"
            clear       To clear the text
            restart     To reboot
            shutdown    To turn off the system
            version     To show the current verion";

        }
    }

    internal class Clear : Command
    {
        public Clear(String name) : base(name) { }

        public override String execute(String[] args)
        {
            Console.Clear();
            return "Welcome to ProjectOS\nType \"help\" for basic commands";
        }
    }

    internal class Restart : Command
    {
        public Restart(String name) : base(name) { }

        public override String execute(String[] args)
        {
            String response = "Restarting the system...";
            Console.WriteLine(response);
            Cosmos.System.Power.Reboot();
            return "Welcome to ProjectOS\nType \"help\" for basic commands";
        }
    }

    internal class ShutDown : Command
    {
        public ShutDown(String name) : base(name) { }

        public override String execute(String[] args)
        {
            Cosmos.System.Power.Shutdown();
            return "Shutting down the system...";
        }
    }

    internal class Version : Command
    {
        public Version(String name) : base(name) { }
        public override String execute(String[] args)
        {
            return "ProjectOS v0.0.1";
        }
    }
}
