using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using ProjectOS.Commands;
using Cosmos.System.FileSystem;

namespace ProjectOS{
    public class Kernel : Sys.Kernel{

        private CommandManager commandManager;
        private CosmosVFS vfs;
          
        protected override void BeforeRun(){

            this.vfs = new CosmosVFS();
            try
            {
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(this.vfs);
                Console.WriteLine("CosmosVFS initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing CosmosVFS: {ex.Message}");
            }
            this.commandManager = new CommandManager();

            Console.WriteLine("Welcome to ProjectOS");
            
        }

        protected override void Run(){
            while (true)
            {
                String response;
                Console.Write(">: ");
                String input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }
                response = this.commandManager.processInput(input);
                Console.WriteLine(response);
            }

        }
    }
}
