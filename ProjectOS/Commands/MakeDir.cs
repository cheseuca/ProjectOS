using System;
using Cosmos.System.FileSystem.VFS;

namespace ProjectOS.Commands
{
    // MakeDir is a command class for creating a new directory.
    internal class MakeDir : Command
    {
        // Constructor for MakeDir command.
        public MakeDir(String name) : base(name) { }

        // Execute the MakeDir command.
        public override String execute(String[] args)
        {
            // Print out the length and values of command-line arguments for debugging.
            Console.WriteLine("### args length: " + args.Length);
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"### args[{i}]: '{args[i]}'");
            }

            // Check if the command has the required arguments.
            if (args.Length < 2)
            {
                return "Invalid usage. Please provide the new directory name.";
            }

            // Get the new directory name from command arguments.
            var newDir = args[1];
            Console.WriteLine("### newDir: '" + newDir + "'"); // Add this line for debugging

            try
            {
                // Attempt to create the directory using VFSManager.
                VFSManager.CreateDirectory(newDir);
                return "Directory created successfully.";
            }
            catch (UnauthorizedAccessException uaex)
            {
                return "Unauthorized access: " + uaex.Message;
            }
            catch (Exception ex)
            {
                return "Failed to create directory: " + ex.Message;
            }
        }

    }
}
