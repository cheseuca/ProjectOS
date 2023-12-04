using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Sys = Cosmos.System;
using System.Reflection.PortableExecutable;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;

namespace ProjectOS.Commands
{
    internal class File : Command
    {
        public static string currentDirectory = @"0:\";
        public File(String name) : base(name)
        {
            //this.description = "Create a file";
            //this.usage = "file [name]";
        }
        public override String execute(String[] args)
        {

            String response = "";
            // string currentDirectory = @"0:\";

            //file commands 
            switch (args[0])
            {
                // change directory 
                case "cd":
                    try
                    {
                        if (args.Length == 2 && args[0].ToLower() == "cd")
                        {
                            string targetDirectory = args[1];

                            // Check for special case: cd ..
                            if (targetDirectory == "..")
                            {
                                // Get the parent directory
                                string parentDirectory = Path.GetDirectoryName(currentDirectory);

                                // Check if the parent directory is not null (not at the root)
                                if (!string.IsNullOrEmpty(parentDirectory))
                                {
                                    // Update the current directory globally for the class
                                    currentDirectory = parentDirectory;
                                    response = "Current directory changed to: " + currentDirectory;
                                }
                                else
                                {
                                    response = "Already at the root directory. Cannot go back further.";
                                }
                            }
                            else
                            {
                                // Normal cd command
                                string newDirectory = Path.Combine(currentDirectory, targetDirectory);
                                if (Sys.FileSystem.VFS.VFSManager.DirectoryExists(newDirectory))
                                {
                                    // Update the current directory globally for the class
                                    currentDirectory = newDirectory;
                                    response = "Current directory changed to: " + currentDirectory;
                                }
                                else
                                {
                                    response = "Directory does not exist: " + newDirectory;
                                }
                            }
                        }
                        else
                        {
                            response = "Invalid 'cd' command format. Usage: file cd <directory> or file cd ..";
                        }
                    }
                    catch (Exception ex)
                    {
                        response = "Exception: " + ex.Message;
                    }
                    break;

                // create file
                case "mkfile":
                    try
                    {
                        Sys.FileSystem.VFS.VFSManager.CreateFile(Path.Combine(currentDirectory, args[1]));
                        response = "Your file \"" + args[1] + "\" has been created";
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                        //response = "Your file \"" + args[1] + "\" could not be created";
                    }
                    break;

                // remove file from a directory
                case "rmfile":
                    try
                    {
                        Sys.FileSystem.VFS.VFSManager.DeleteFile(args[1]);
                        response = "Your file \"" + args[1] + "\" has been deleted";
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                        //response = "Your file \"" + args[1] + "\" could not be created";
                    }
                    break;

                // create directory
                case "mkdir":
                    try
                    {
                        Sys.FileSystem.VFS.VFSManager.CreateDirectory(args[1]);
                        response = "Your directory \"" + args[1] + "\" has been created";
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                        //response = "Your file \"" + args[1] + "\" could not be created";
                    }
                    break;

                // remove directory and all files in it
                case "rmdir":
                    try
                    {
                        Sys.FileSystem.VFS.VFSManager.DeleteDirectory(args[1], true);
                        response = "Your directory \"" + args[1] + "\" has been deleted";
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                        //response = "Your file \"" + args[1] + "\" could not be created";
                    }
                    break;

                // write to txt file
                case "writestr":
                    try
                    {
                        string filePath;

                        // Check if arguments are provided
                        if (args.Length > 1)
                        {
                            // If arguments are provided, use the specified file path
                            filePath = args[1];

                            // If the file path is not a full path, combine with the current directory
                            if (!filePath.Contains(":\\"))
                            {
                                filePath = Path.Combine(currentDirectory, filePath);
                            }
                        }
                        else
                        {
                            // If no arguments are provided, use a default file path based on the current directory
                            string fileName = "default.txt";
                            filePath = Path.Combine(currentDirectory, fileName);
                        }

                        // Get the file stream based on the file path
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(filePath).GetFileStream();

                        // Check if the file stream is writable
                        if (fs.CanWrite)
                        {
                            int count = 0;
                            StringBuilder sb = new StringBuilder();

                            // Append all arguments (excluding the command and file path) to the StringBuilder
                            foreach (String s in args.Skip(2))
                            {
                                sb.Append(s);
                                sb.Append(" ");
                            }

                            Byte[] data = Encoding.ASCII.GetBytes(sb.ToString().Trim());
                            fs.Write(data, 0, data.Length);
                            fs.Close();
                            response = "Successfully wrote to file: " + filePath;
                        }
                        else
                        {
                            response = "Unable to write file: Not open for writing";
                        }
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                    }
                    break;

                // read txt file
                case "readstr":
                    try
                    {
                        string filePath;

                        // Check if arguments are provided
                        if (args.Length > 1)
                        {
                            // If arguments are provided, use the specified file path
                            filePath = args[1];

                            // If the file path is not a full path, combine with the current directory
                            if (!filePath.Contains(":\\"))
                            {
                                filePath = Path.Combine(currentDirectory, filePath);
                            }
                        }
                        else
                        {
                            // If no arguments are provided, use a default file path based on the current directory
                            string fileName = "default.txt";
                            filePath = Path.Combine(currentDirectory, fileName);
                        }

                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(filePath).GetFileStream();

                        if (fs.CanRead)
                        {
                            Byte[] data = new Byte[256];
                            fs.Read(data, 0, data.Length);
                            response = Encoding.ASCII.GetString(data);
                        }
                        else
                        {
                            response = "Unable to read from file: Not open for reading";
                        }
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                    }
                    break;


                // show available space in the disk
                case "space":
                    try
                    {
                        response = "Available space: " + Sys.FileSystem.VFS.VFSManager.GetAvailableFreeSpace(args[1]);
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }
                    break;

                //// show list of directories in disk
                //case "lsdir":
                //    try
                //    {
                //        List<Cosmos.System.FileSystem.Listing.DirectoryEntry> entryList = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(args[1]);

                //        // Filter directories and select only their names
                //        List<string> directoryNames = entryList
                //            .Where(entry => entry.mEntryType == Cosmos.System.FileSystem.Listing.DirectoryEntryTypeEnum.Directory)
                //            .Select(entry => entry.mName)
                //            .ToList();

                //        Console.WriteLine("Directory list: {0}:", args[1]);
                //        Console.WriteLine(new string('=', 73));

                //        foreach (var dirName in directoryNames)
                //        {
                //            Console.WriteLine($"=   > {dirName}");
                //        }

                //        Console.WriteLine(new string('=', 73));
                //    }
                //    catch (Exception ex)
                //    {
                //        response = ex.ToString();
                //        break;
                //    }
                //    break;

                //case "lsfile":
                //    try
                //    {
                //        if (args.Length == 1)
                //        {
                //            // No directory specified, show current directory files
                //            var fileList = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(currentDirectory);

                //            Console.WriteLine("File list in current directory:");
                //            Console.WriteLine(new string('=', 73));

                //            foreach (var fileEntry in fileList)
                //            {
                //                if (fileEntry.mEntryType == Cosmos.System.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                //                {
                //                    Console.WriteLine($"=   > {fileEntry.mName}");
                //                }
                //            }

                //            Console.WriteLine(new string('=', 73));
                //        }
                //        else if (args.Length == 2)
                //        {
                //            // Directory specified, show files in that directory
                //            string targetDirectory = args[1];
                //            if (!targetDirectory.Contains(":\\"))
                //            {
                //                // If the target directory is not a full path, combine with the current directory
                //                targetDirectory = Path.Combine(currentDirectory, targetDirectory);
                //            }

                //            var fileList = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(targetDirectory);

                //            Console.WriteLine($"File list in directory: {targetDirectory}:");
                //            Console.WriteLine(new string('=', 73));

                //            foreach (var fileEntry in fileList)
                //            {
                //                if (fileEntry.mEntryType == Cosmos.System.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                //                {
                //                    Console.WriteLine($"=   > {fileEntry.mName}");
                //                }
                //            }

                //            Console.WriteLine(new string('=', 73));
                //        }
                //        else
                //        {
                //            // Incorrect number of arguments
                //            Console.WriteLine("Usage: file lsfile [directory]");
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        response = ex.ToString();
                //    }
                //    break;

                // show list of directories in disk
                case "lsdir":
                    try
                    {
                        string targetDirectory;

                        // Check if arguments are provided
                        if (args.Length > 1)
                        {
                            targetDirectory = args[1];
                        }
                        else
                        {
                            // If no arguments are provided, use the current directory
                            targetDirectory = currentDirectory;
                        }

                        List<Cosmos.System.FileSystem.Listing.DirectoryEntry> entryList = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(targetDirectory);

                        // Filter directories and select only their names
                        List<string> directoryNames = entryList
                            .Where(entry => entry.mEntryType == Cosmos.System.FileSystem.Listing.DirectoryEntryTypeEnum.Directory)
                            .Select(entry => entry.mName)
                            .ToList();

                        Console.WriteLine("Directory list: {0}:", targetDirectory);
                        Console.WriteLine(new string('=', 73));

                        foreach (var dirName in directoryNames)
                        {
                            Console.WriteLine($"=   > {dirName}");
                        }

                        Console.WriteLine(new string('=', 73));
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                    }
                    break;

                // show list of files in directory
                case "lsfile":
                    try
                    {
                        string targetDirectory;

                        // Check if arguments are provided
                        if (args.Length == 1)
                        {
                            // No directory specified, show current directory files
                            targetDirectory = currentDirectory;
                        }
                        else if (args.Length == 2)
                        {
                            // Directory specified, show files in that directory
                            targetDirectory = args[1];

                            // If the target directory is not a full path, combine with the current directory
                            if (!targetDirectory.Contains(":\\"))
                            {
                                targetDirectory = Path.Combine(currentDirectory, targetDirectory);
                            }
                        }
                        else
                        {
                            // Incorrect number of arguments
                            Console.WriteLine("Usage: file lsfile [directory]");
                            break;
                        }

                        var fileList = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(targetDirectory);

                        Console.WriteLine($"File list in directory: {targetDirectory}:");
                        Console.WriteLine(new string('=', 73));

                        foreach (var fileEntry in fileList)
                        {
                            if (fileEntry.mEntryType == Cosmos.System.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                            {                        
                                Console.WriteLine($"=   > {fileEntry.mName}");
                            }
                        }

                        Console.WriteLine(new string('=', 73));
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                    }
                    break;

                // move the file from default directory to another directory
                case "mvfile":
                    try
                    {
                        if (args.Length >= 3 && args[0].ToLower() == "mvfile")
                        {
                            string sourceFile = Path.Combine(currentDirectory, args[1]);
                            string destinationDirectory = Path.Combine(currentDirectory, args[2]);

                            // Check if the source file exists
                            if (Sys.FileSystem.VFS.VFSManager.FileExists(sourceFile))
                            {
                                // Check if the destination directory exists
                                if (Sys.FileSystem.VFS.VFSManager.DirectoryExists(destinationDirectory))
                                {
                                    // Build the full path for the destination file
                                    string destinationFile = Path.Combine(destinationDirectory, args[1]);

                                    // Read content from the source file
                                    string fileContent;
                                    using (var reader = new StreamReader(sourceFile))
                                    {
                                        fileContent = reader.ReadToEnd();
                                    }

                                    // Write content to the destination file
                                    using (var writer = new StreamWriter(destinationFile))
                                    {
                                        writer.Write(fileContent);
                                    }

                                    // Optionally, delete the original file
                                    Sys.FileSystem.VFS.VFSManager.DeleteFile(sourceFile);

                                    response = "File moved successfully to: " + destinationFile;
                                }
                                else
                                {
                                    response = "Destination directory does not exist: " + destinationDirectory;
                                }
                            }
                            else
                            {
                                response = "Source file does not exist: " + sourceFile;
                            }
                        }
                        else
                        {
                            response = "Invalid 'mvfile' command format. Usage: file mvfile <source_file> <destination_directory>";
                        }
                    }
                    catch (Exception ex)
                    {
                        response = "Exception: " + ex.Message;
                    }
                    break;

                default:
                    response = "Unexpected argument: " + args[0];
                    break;
            }
            return response;
        }
    }
}
