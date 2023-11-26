using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using ProjectOS.Commands;
using Cosmos.System.FileSystem;

namespace ProjectOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        private CommandManager commandManager;
        private CosmosVFS vfs;
        public static DateTime BootTime { get; private set; }

        protected override void BeforeRun()
        {
            BootTime = DateTime.Now;
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
            

            Console.WriteLine("\r\n     _    _      _                           \r\n    | |  | |    | |                          \r\n    | |  | | ___| | ___ ___  _ __ ___   ___  \r\n    | |/\\| |/ _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\ \r\n    \\  /\\  /  __/ | (_| (_) | | | | | |  __/ \r\n     \\/  \\/ \\___|_|\\___\\___/|_| |_| |_|\\___| \r\n                                             \r\n                                             \r\n                  _____                      \r\n                 |_   _|                     \r\n                  | | ___                   \r\n                   | |/ _ \\                  \r\n                   | | (_) |                 \r\n                   \\_/\\___/                  \r\n                                             \r\n                                             \r\n      ______                _____ _____      \r\n      | ___ \\              |  _  /  ___|     \r\n      | |_/ /__ _ _ __ ___ | | | \\ `--.      \r\n      |    // _` | '_ ` _ \\| | | |`--. \\     \r\n      | |\\ \\ (_| | | | | | \\ \\_/ /\\__/ /     \r\n      \\_| \\_\\__,_|_| |_| |_|\\___/\\____/      \r\n                                             \r\n                                            \r\n");

            Console.WriteLine("Type 'help' for a list of commands\n");
        }

        protected override void Run()
        {
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

        //protected override void AfterRun()
        //{
        //    Console.WriteLine("Shutdown");
        //}
    }
}
