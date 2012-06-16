using System;
using System.Text;
using CommandLine;

namespace KangaModeling.CommandLine
{

    /// <summary>
    /// Provides cmd line parsing via CommandLine library.
    /// </summary>
    internal sealed class Options
    {
        [Option("o", "output", Required = false, HelpText = "output file to write image to")]
        public String FileName = "out.png";

        [Option("f", "format", Required = false, HelpText = "format of output")]
        public String Format = "png";

        [Option("m", "model", Required = true, HelpText = "the model to convert")]
        public String Model = String.Empty;

        [HelpOption(HelpText = "display the help text")]
        public string GetHelp()
        {
            var help = new StringBuilder();
            help.AppendLine("kanga - models from text");
            help.AppendLine("-o filename - output file");
            help.AppendLine("-f format - output file format (png,bmp,jpeg)");
            help.AppendLine("-m model - the model definition");
            return help.ToString();
        }

        public override string ToString()
        {
            return String.Format("file={0}, format={1}, model={2}", FileName, Format, Model);
        }

    }
}
