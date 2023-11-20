using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using System;

namespace ProjectOS.Commands
{
    internal class ChangeDirectory : Command
    {
        public ChangeDirectory(String name) : base(name) { }

        public override String execute(String[] args)
        {
            // Check if the correct number of arguments has been provided
            if (args.Length != 1)
            {
                return "Usage: cd <directory>";
            }

            // Get the directory path from the arguments
            string directoryPath = args[0];

            // Check if the directory exists
            if (!Directory.Exists(directoryPath))
            {
                // If the directory does not exist, create it
                VFSManager.CreateDirectory(directoryPath);
            }

            // Change the current directory
            Directory.SetCurrentDirectory(directoryPath);

            // Return a success message
            return "Current directory changed to: " + directoryPath;
        }
    }
}
