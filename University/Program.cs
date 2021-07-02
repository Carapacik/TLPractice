using System;

namespace University
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Enter a command. For more info enter 'help'");
            var handler = new CommandHandler(Console.Out, Console.In);
            handler.Start();
        }
    }
}