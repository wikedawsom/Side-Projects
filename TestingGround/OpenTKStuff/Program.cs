using System;


namespace OpenTKStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var window = new Window(800, 450, "Big Test"))
            {
                window.Run(60.0);
            }
        }
    }
    
}
