using System;
using System.Collections.Generic;
using System.IO;

namespace Befunge
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: befunge <source.bf>");
                Environment.Exit(1);
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"Invalid filename: {args[0]}");
                Environment.Exit(1);
            }

            var source = File.OpenText(args[0]).ReadToEnd();
            Console.WriteLine(source);

            var tokenizer = new Tokenizer(source);
            var tokens = tokenizer.Tokenize();
            var field = new PlayField(tokens);
            Console.WriteLine(field);
        }
    }

    public class VirtualMachine 
    {
        private readonly PlayField _playField;

        public VirtualMachine(PlayField playField) 
        {
            _playField = playField;
        }

        public void Run() {

        }
    }
}
