using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using ProjectOS.Commands;
using Cosmos.System.FileSystem;
using ProjectOS.Graphics;

namespace ProjectOS{
    public class Kernel : Sys.Kernel{

        private CommandManager commandManager;
        private CosmosVFS vfs;
        public static GUI gui;


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

            Console.Clear();

            Console.WriteLine("Welcome to ProjectOS\nType \"help\" for basic commands");
            
        }

        protected override void Run(){

            if (Kernel.gui != null)
            {

                Kernel.gui.handleGUIInputs();
                return;
            }
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
