using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using ProjectOS.Commands;
using Cosmos.System.FileSystem;
using Cosmos.HAL;

namespace ProjectOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        private CommandManager commandManager;
        private CosmosVFS vfs;

        public static int bootHour { get; private set; }
        public static int bootMinute { get; private set; }
        public static int bootSecond { get; private set; }
        public static string bootTime { get; private set; }


        protected override void BeforeRun()
        {
            bootHour = RTC.Hour;
            bootMinute = RTC.Minute;
            bootSecond = RTC.Second;
            bootTime = $"Boot Time: {bootHour:D2}:{bootMinute:D2}:{bootSecond:D2}";

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
            Console.Write("Username: ");
            string usernameInput = Console.ReadLine();

            Console.Write("Password: ");
            string passwordInput = ReadPasswordInput();

            if (AuthenticateUser(usernameInput, passwordInput))
            {
                Console.WriteLine("Authentication successful!");

                // Continue with the rest of the code or execute specific commands for authenticated users
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
            else
            {
                Console.WriteLine("Authentication failed. Please try again.");
            }
        }

        private string ReadPasswordInput()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                // Process backspace
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop); 
                    Console.Write(" "); 
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop); 
                    password.Remove(password.Length - 1, 1);
                }
                // Ignore any key that is not a printable character or backspace
                else if (!char.IsControl(key.KeyChar))
                {
                    password.Append(key.KeyChar);
                    Console.Write("*"); // Display '*' instead of the actual character
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Move to the next line after entering the password
            return password.ToString();
        }


        private bool AuthenticateUser(string username, string password)
        {
            // For demonstration purposes, using hardcoded values (user = admin, password = adminisme)
            return username == "admin" && password == "adminisme";
        }



    }
}
