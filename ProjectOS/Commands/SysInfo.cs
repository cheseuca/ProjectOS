using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Cosmos.Core;
using ProjectOS;
using Cosmos.HAL;
using Cosmos.System.Graphics;
using Cosmos.System.Network;
using System.Net.Sockets;
using System.Net;

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
        public static bool IsVirtualized { get; set; } = true;


        public SysInfo()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("System Specifications:");
            Console.WriteLine($"CPU: {CPU.GetCPUBrandString()}");
            Console.WriteLine($"CPU Cores: {CPU.GetCPUCycleSpeed()}");
            Console.WriteLine($"RAM: {usedmem}/{maxmem}MB");
            Console.WriteLine($"Resolution: {Console.WindowWidth} x {Console.WindowHeight}");

            // Check if the system is virtualized (based on user configuration)
            Console.WriteLine($"Is Virtualized: {IsVirtualized}");

            Console.WriteLine($"Time at boot: {Kernel.bootTime}");
            Console.WriteLine($"Uptime: {uptimeHour:D2}:{uptimeMinute:D2}:{uptimeSecond:D2}");
            Console.WriteLine("Kernel Version: 1.0.0");
            Console.WriteLine();
            Console.ResetColor();
        }
        
    }
}
