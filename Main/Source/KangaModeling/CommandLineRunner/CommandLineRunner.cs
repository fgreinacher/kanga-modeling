using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Compiler.SequenceDiagrams;
using CommandLine;
using System.Drawing.Imaging;
 

namespace CommandLineRunner
{

    /// <summary>
    /// Main class for running KangaModeling from cmd line.
    /// </summary>
    public sealed class CommandLineRunner
    {

        public static void Main(String[] args)
        {
            Options opts = new Options();
            if (!new CommandLineParser().ParseArguments(args, opts))
            {
                System.Console.Error.WriteLine(opts.GetHelp());
                return;
            }
            new CommandLineRunner().run(opts);
        }

        internal void run(Options opts)
        {
            SequenceDiagram sd = Parser.ParseString(opts.Model);

            // TODO make an image and put somewhere?!
        }

    }
}
