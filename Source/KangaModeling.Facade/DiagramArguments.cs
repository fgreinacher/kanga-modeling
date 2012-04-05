using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Facade
{
	/// <summary>
	/// Represents the arguments needed to create a diagram.
	/// </summary>
	public class DiagramArguments
	{
		public DiagramArguments(string text, DiagramType type, DiagramStyle style)
		{
			Text = text;
			Type = type;
			Style = style;
		}

		public string Text { get; private set; }

		public DiagramType Type { get; private set; }

		public DiagramStyle Style { get; private set; }
	}
}
