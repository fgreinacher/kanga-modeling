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
		public DiagramArguments(string source, DiagramType type, DiagramStyle style, bool debug = false)
		{
			Source = source;
			Type = type;
			Style = style;
            Debug = debug;
		}

		public string Source { get; private set; }

		public DiagramType Type { get; private set; }

        public DiagramStyle Style { get; private set; }

        public bool Debug { get; private set; }
	}
}
