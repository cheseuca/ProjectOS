using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.Listing;

namespace ProjectOS.Commands
{
    internal class Directory : Command
    {
        // Show the existing directory
        public Directory(String name) : base(name) { }

        internal static bool Exists(string directoryPath)
        {
            throw new NotImplementedException();
        }

        internal static void SetCurrentDirectory(string directoryPath)
        {
            throw new NotImplementedException();
        }

        public override String execute(String[] args)
        {
            return "";
        }
    }
}
