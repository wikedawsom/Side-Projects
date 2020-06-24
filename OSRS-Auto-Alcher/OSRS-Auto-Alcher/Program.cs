using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;

namespace OSRS_Auto_Alcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of items to alch: ");
            string input = Console.ReadLine();
            try
            { 
                Alch(int.Parse(input));
            }
            catch
            {
                Console.WriteLine("Can't understand that. Defaulting to 125.");
                Alch(125);
            }
            Console.WriteLine("Done");
        }
        static void Alch(int count)
        {
            Random rand = new Random();
            int delay = rand.Next() % 1000 + 3000;
            AdbInput(110, 650);
            Thread.Sleep(rand.Next() % 500 + 500);
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"Alching {i + 1}/{count}");
                AdbInput(500, 680);
                Thread.Sleep(rand.Next() % 350 + 1100);

                AdbInput(2100, 650);
                delay = rand.Next() % 500 + 2100;
                Thread.Sleep(delay);
            }
        }
        static void AdbInput(int x, int y)
        {
            ProcessStartInfo adb_params = new ProcessStartInfo
            {
                FileName = @"C:\Users\cp_ha\AppData\Local\Android\Sdk\platform-tools\adb.exe",
                Arguments = $"shell input tap {x} {y}"
            };
            Process.Start(adb_params);
        }
    }
}
