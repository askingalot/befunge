using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Befunge
{
    class Program
    {
        static void Main(string[] args)
        {
            var debug = args.Any(arg => arg == "--debug");
            var filename = args.FirstOrDefault(arg => !arg.StartsWith("--"));

            if (filename == null)
            {
                Console.WriteLine("Usage: befunge <source.bf> [--debug]");
                Environment.Exit(1);
            }

            if (!File.Exists(filename))
            {
                Console.WriteLine($"Invalid filename: {args[0]}");
                Environment.Exit(1);
            }

            var source = File.OpenText(args[0]).ReadToEnd();
            if (debug) {
                Console.Error.WriteLine(source);
            }

            if (debug) {
                Console.Error.WriteLine("Tokenizing...");
            }
            var tokenizer = new Tokenizer(source);
            var tokens = tokenizer.Tokenize();

            if (debug) {
                Console.Error.WriteLine("Creating the playing field...");
            }
            var field = new PlayField(tokens);

            if (debug) {
                Console.Error.WriteLine("Running the VM...");
            }
            new VirtualMachine().Run(field, debug);

            Console.WriteLine();
        }
    }
}
