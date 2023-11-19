using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using ProjectOS.Commands;

namespace ProjectOS{
    public class Kernel : Sys.Kernel{

        private CommandManager commandManager;
          
        protected override void BeforeRun(){
            Console.WriteLine("Welcome to ProjectOS");
            this.commandManager = new CommandManager();
        }

        protected override void Run(){
            String response;
            String input = Console.ReadLine();
            response = this.commandManager.processInput(input);
            Console.WriteLine(response);

        }
    }
}
