using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KangaModeling.Facade
{
	/// <summary>
	/// Represents the result of a diagram creation.
	/// When the result is disposed, the created image is disposed as well.
	/// </summary>
	public class DiagramResult : IDisposable
	{
		public DiagramResult(DiagramArguments arguments, Image image, DiagramError[] errors)
		{
			Arguments = arguments;
			Image = image;
			Errors = errors;
		}

		public DiagramArguments Arguments { get; private set; }

		public Image Image { get; private set; }

		public IEnumerable<DiagramError> Errors { get; private set; }

		public void Dispose()
		{
			Image.Dispose();
		}
	}
}
