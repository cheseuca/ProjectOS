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
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(args[1]).GetFileStream();

                        if (fs.CanWrite)
                        {
                            int count = 0;
                            StringBuilder sb = new StringBuilder();
                            foreach (String s in args)
                            {
                                if (count != 0 && count != 1)
                                {
                                    sb.Append(s);
                                    sb.Append(" ");
                                }
                                ++count;
                            }

                            Byte[] data = Encoding.ASCII.GetBytes(sb.ToString());
                            fs.Write(data, 0, data.Length);
                            fs.Close();
                            response = "Succesfully wrote to file";
                        }

                        else
                        {
                            response = "Unable to write file: Not open for writing";
                            break;
                        }
                    }

                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }
                    break;

                // read txt file 
                case "readstr":

                    try
                    {
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(args[1]).GetFileStream();

                        if (fs.CanRead)
                        {
                            Byte[] data = new Byte[256];
                            fs.Read(data, 0, data.Length);
                            response = Encoding.ASCII.GetString(data);

                        }

                        else
                        {
                            response = "Unable to reawde from file: Not open for reading";
                        }
                    }

                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
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

                // show list of directories in disk
                case "lsdir":
                    try
                    {
                        List<Cosmos.System.FileSystem.Listing.DirectoryEntry> entryList = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(args[1]);

                        // Filter directories and select only their names
                        List<string> directoryNames = entryList
                            .Where(entry => entry.mEntryType == Cosmos.System.FileSystem.Listing.DirectoryEntryTypeEnum.Directory)
                            .Select(entry => entry.mName)
                            .ToList();

                        Console.WriteLine("Directory list: {0}:", args[1]);
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
                        break;
                    }
                    break;

                //// show list of files in directory
                //case "lsfile":
                //    try
                //    {
                //        var fileList = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(args[1]);

                //        Console.WriteLine("File list in directory: {0}:", args[1]);
                //        Console.WriteLine(new string('=', 73));

                //        foreach (var fileEntry in fileList)
                //        {
                //            if (fileEntry.mEntryType == Cosmos.System.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                //            {
                //                Console.WriteLine($"=   > {fileEntry.mName}");
                //            }
                //        }

                //        Console.WriteLine(new string('=', 73));
                //    }
                //    catch (Exception ex)
                //    {
                //        response = ex.ToString();
                //        break;
                //    }
                //    break;

                // show list of files in directory
                case "lsfile":
                    try
                    {
                        if (args.Length == 1)
                        {
                            // No directory specified, show current directory files
                            var fileList = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(currentDirectory);

                            Console.WriteLine("File list in current directory:");
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
                        else if (args.Length == 2)
                        {
                            // Directory specified, show files in that directory
                            string targetDirectory = args[1];
                            if (!targetDirectory.Contains(":\\"))
                            {
                                // If the target directory is not a full path, combine with the current directory
                                targetDirectory = Path.Combine(currentDirectory, targetDirectory);
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
                        else
                        {
                            // Incorrect number of arguments
                            Console.WriteLine("Usage: file lsfile [directory]");
                        }
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
