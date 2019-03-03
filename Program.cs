using System;
using System.Collections.Generic;
using System.IO;

namespace Befunge
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 ) {
                Console.WriteLine("Usage: befunge <source.bf>");
                Environment.Exit(1);
            }
            if (!File.Exists(args[0])) {
                Console.WriteLine($"Invalid filename: {args[0]}");
                Environment.Exit(1);
            }

            var source = File.OpenText(args[0]).ReadToEnd();
            Console.WriteLine(source);

            var tokenizer = new Tokenizer(source);

            foreach(var token in tokenizer.Tokenize()) {
                Console.WriteLine(token);
            }
        }
    }

}
