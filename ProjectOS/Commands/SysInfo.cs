using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Cosmos.Core;
using ProjectOS;
using Cosmos.HAL;

namespace ProjectOS.Commands
{
    public class SysInfo
    {
        static uint maxmem = CPU.GetAmountOfRAM();
        static ulong availableMem = GCImplementation.GetAvailableRAM();
        static ulong usedmem = maxmem - availableMem;
        static ulong cpuUptime = CPU.GetCPUUptime();
        static int uptimeHour = RTC.Hour - Kernel.bootHour;
        static int uptimeMinute = RTC.Minute - Kernel.bootMinute;
        static int uptimeSecond = RTC.Second - Kernel.bootSecond;

        public SysInfo()
            {
            Console.WriteLine();
            Console.WriteLine("System Specifications:", ConsoleColor.Yellow);
            Console.WriteLine($"CPU: {CPU.GetCPUBrandString()}", ConsoleColor.Yellow);
            Console.WriteLine($"RAM: {usedmem}/{maxmem}MB", ConsoleColor.Yellow);
            Console.WriteLine($"Time at boot: {Kernel.bootTime}", ConsoleColor.Yellow);
            Console.WriteLine($"Uptime: {uptimeHour:D2}:{uptimeMinute:D2}:{uptimeSecond:D2}", ConsoleColor.Yellow);

            // Check for null before accessing BootTime
            //if (Kernel.BootTime != DateTime.MinValue)
            //{
            //    Console.WriteLine($"Time at boot: {DateTime.Now}", ConsoleColor.Yellow);
            //    TimeSpan uptime = DateTime.Now - Kernel.BootTime;
            //    Console.WriteLine($"Uptime: {uptime.Days} days, {uptime.Hours} hours, {uptime.Minutes} minutes", ConsoleColor.Yellow);
            //}
            //else
            //{
            //    Console.WriteLine("Error retrieving boot time.", ConsoleColor.Red);
            //}
            //TimeSpan uptime = DateTime.Now - Kernel.BootTime;
            //Console.WriteLine($"Uptime: {uptime.Days} days, {uptime.Hours} hours, {uptime.Minutes} minutes", ConsoleColor.Yellow);
        }
        
    }
}
