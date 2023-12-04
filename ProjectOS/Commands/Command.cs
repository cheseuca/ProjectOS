using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.HAL;
using ProjectOS;

namespace ProjectOS.Commands{
    public class Command{
        
        public readonly string name;

        public static SysInfo info;

        //Kernel DefaultMessage = new Kernel();
        public Command(string name)
        {
            this.name = name;
        }

        public virtual String execute(String[] args)
        {
            return "Command not found";
        }
    }

    internal class Help : Command
    {

        public Help(String name) : base(name) { }

        public override String execute(String[] args)
        {
            return @"
            clear       To clear the text
            restart     To reboot
            shutdown    To turn off the system
            sysinfo     To show the system info
            foreground  To change the foreground color 
            background  To change the background color
            
            file        file manamgement
            input: file + command

            command:
            cd           change directory    |  mkfile      make file 
            rmdir        remove directory    |  rmfile      remove file
            writestr     write a text file   |  mvfile      move file
            space        remaining space     |  readstr     read text file
            lsdir        list of directories |  mkdir       make directory
            lsfile       list of files       | 
            
            
            calc         open the calculator app
            hangman      play hangman
            feelinglucky play guess the number
            xoxo         play tictactoe";
        }
    }

    public class Clear : Command
    {
        public Clear(String name) : base(name) { }

        public override String execute(String[] args)
        {
            Console.Clear();
            Kernel.DisplayDefaultMessage();
            return "Console cleared.";
        }
    }

    internal class Restart : Command
    {
        public Restart(String name) : base(name) { }

        public override String execute(String[] args)
        {
            String response = "Restarting the system...";
            Console.WriteLine(response);
            Cosmos.System.Power.Reboot();
            Kernel.DisplayDefaultMessage();
            return "";
        }
    }

    internal class ShutDown : Command
    {
        public ShutDown(String name) : base(name) { }

        public override String execute(String[] args)
        {
            Cosmos.System.Power.Shutdown();
            return "Shutting down the system...";
        }
    }

    internal class getSysInfo : Command
    {
        public getSysInfo (String name) : base(name) { }

        public override string execute(string[] args)
        {
            Command.info = new SysInfo();
            return "System Info Generated";
        }
    }

    internal class DateTime : Command
    {
        public DateTime(String name) : base(name) { }

        public override string execute(string[] args)
        {
            int hour = RTC.Hour;
            int minute = RTC.Minute;
            int second = RTC.Second;
            string date = RTC.DayOfTheMonth.ToString() + "/" + RTC.Month.ToString() + "/" + RTC.Year.ToString();
            string formattedDateTime = $"Current Time: {hour:D2}:{minute:D2}:{second:D2}" + "\n" + "Current Date: " +date;
            return formattedDateTime;
        }
    }
}
