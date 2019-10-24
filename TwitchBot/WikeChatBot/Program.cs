using System;

namespace WikeBot
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Party time");
            ChatBot bot1 = new ChatBot();
            bot1.Run();
            Console.WriteLine("**I've been listening all day, let me rest**");
            Console.ReadKey();
        }
    }
}
