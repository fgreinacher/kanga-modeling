using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Compiler.SequenceDiagrams;
 

namespace CommandLineRunner
{
    public sealed class CommandLineRunner
    {

        public static void Main(String[] args)
        {
            new CommandLineRunner().run(args);
        }

        public void run(String[] args)
        {
            if (args.Length == 0)
                return;
            // TODO -f means "file argument"
            SequenceDiagram sd = Parser.ParseString(args[0]);

            // TODO make an image and put somewhere?!
        }

    }
}
