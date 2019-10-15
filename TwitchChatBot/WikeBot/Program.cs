using System;

namespace WikeBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Party time");
            BotMaster.MainLoop();

            Console.WriteLine("**I've been listening all day, let me rest**");
            Console.ReadKey();
        }
    }
}
