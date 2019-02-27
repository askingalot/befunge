using System;

namespace befunge
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 ) {
                Console.WriteLine("Usage: befunge <source.bf>");
                Environment.Exit(1);
            }
            Console.WriteLine($"run {args[0]}");
        }
    }
}
