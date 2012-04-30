using System;
using CommandLine;
using System.Drawing.Imaging;
using System.Drawing;
using KangaModeling.Facade;

namespace CommandLineRunner
{

	/// <summary>
	/// Main class for running KangaModeling from cmd line.
	/// </summary>
	public sealed class CommandLineRunner
	{

		public static int Main(String[] args)
		{
			Options opts = new Options();
			if (!new CommandLineParser().ParseArguments(args, opts))
			{
				Console.Error.WriteLine(opts.GetHelp());
				return -1;
			}
			new CommandLineRunner().Run(opts);
			return 0;
		}

		internal void Run(Options opts)
		{
			var arguments = new DiagramArguments(opts.Model, DiagramType.Sequence, DiagramStyle.Sketchy);
			var result = DiagramFactory.Create(arguments);
			using (result)
			{
				ImageFormat format = ImageFormat.Png;
				switch (opts.Format.ToLowerInvariant())
				{
					case "png":
						format = ImageFormat.Png;
						break;
					case "bmp":
						format = ImageFormat.Bmp;
						break;
					case "jpeg":
						format = ImageFormat.Jpeg;
						break;
					default:
						throw new ArgumentException("unknown format: " + opts.Format);
				}
				result.Image.Save(opts.FileName, format);
			}
		}
	}
}
