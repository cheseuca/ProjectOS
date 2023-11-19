using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Sys = Cosmos.System;

namespace ProjectOS.Commands{
    internal class File : Command{
        public File(String name) : base(name)
        {
            //this.description = "Create a file";
            //this.usage = "file [name]";
        }
        public override String execute(String[] args) { 
            
            String response = "";

           //file create MyFile.txt
           switch (args[0])
            {
                case "create":
                    try{
                        Sys.FileSystem.VFS.VFSManager.CreateFile(args[1]);
                        response = "Your file \"" + args[1] + "\" has been created";
                    }
                    catch (Exception ex){
                        response = ex.ToString();
                        break;
                        //response = "Your file \"" + args[1] + "\" could not be created";
                    }
                    break;

                case "del":
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

                case "createdir":
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

                case "deldir":
                    try
                    {
                        Sys.FileSystem.VFS.VFSManager.DeleteDirectory(args[1], true) ;
                        response = "Your directory \"" + args[1] + "\" has been deleted";
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                        //response = "Your file \"" + args[1] + "\" could not be created";
                    }
                    break;

                case "writestr":

                    try{
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(args[1]).GetFileStream();

                        if (fs.CanWrite){
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

                        else{
                            response = "Unable to write file: Not open for writing";
                            break;
                        }
                    }

                    catch (Exception ex){
                        response = ex.ToString();
                        break;
                    }
                    break;

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

                    catch (Exception ex) {
                        response= ex.ToString();
                        break;
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
