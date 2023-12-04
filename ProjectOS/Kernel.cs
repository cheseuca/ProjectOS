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
            
            DisplayWelcomeMessage();

            
        }

        protected override void Run()
        {
            Console.Write("Username: ");
            string usernameInput = Console.ReadLine();

            Console.Write("Password: ");
            string passwordInput = ReadPasswordInput();

            if (AuthenticateUser(usernameInput, passwordInput))
            {   
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Authentication successful!\n");
                Console.ResetColor();

                //add a delay before it clears the screen
                System.Threading.Thread.Sleep(1000);
                Console.Clear();

                DisplayDefaultMessage();

                // Continue with the rest of the code or execute specific commands for authenticated users
                while (true)
                {
                    String response;
                    Console.Write(File.currentDirectory);
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Authentication failed. Please try again.\n");
                Console.ResetColor();
            }
        }

        private static string ReadPasswordInput()
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

        private void DisplayWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            TypeWithDelay("\r\n      _      __        __                           \r\n     | | /| / / ___   / / ____ ___   __ _  ___      \r\n     | |/ |/ / / -_) / / / __// _ \\ /  ' \\/ -_)     \r\n     |__/|__/  \\__/ /_/  \\__/ \\___//_/_/_/\\__/      \r\n                    ______                          \r\n                   /_  __/ ___                      \r\n                    / /   / _ \\                     \r\n            ___    /_/    \\___/____    ____         \r\n           / _ \\ ___ _  __ _  / __ \\  / __/         \r\n          / , _// _ `/ /  ' \\/ /_/ / _\\ \\           \r\n         /_/|_| \\_,_/ /_/_/_/\\____/ /___/           \r\n                                                    \r\n", delayMilliseconds: 1);
            //Console.WriteLine("");
            Console.ResetColor();
            TypeWithDelay("=================================================", delayMilliseconds: 10);
            TypeWithDelay("Type your username and password to log in.", delayMilliseconds: 10);
            TypeWithDelay("For demonstration purposes, use 'admin' \nas the username and 'adminisme' as the password.", delayMilliseconds: 10);
            TypeWithDelay("=================================================", delayMilliseconds: 10);
            Console.WriteLine();
        }

        private void TypeWithDelay(string text, int delayMilliseconds)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(delayMilliseconds);
            }
            Console.WriteLine(); // Move to the next line after typing is complete
        }

        public static void DisplayDefaultMessage()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\r\n      _      __        __                           \r\n     | | /| / / ___   / / ____ ___   __ _  ___      \r\n     | |/ |/ / / -_) / / / __// _ \\ /  ' \\/ -_)     \r\n     |__/|__/  \\__/ /_/  \\__/ \\___//_/_/_/\\__/      \r\n                    ______                          \r\n                   /_  __/ ___                      \r\n                    / /   / _ \\                     \r\n            ___    /_/    \\___/____    ____         \r\n           / _ \\ ___ _  __ _  / __ \\  / __/         \r\n          / , _// _ `/ /  ' \\/ /_/ / _\\ \\           \r\n         /_/|_| \\_,_/ /_/_/_/\\____/ /___/           \r\n                                                    \r\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Type 'help' for a list of commands\n");
            Console.ResetColor();
        }
    }
}
