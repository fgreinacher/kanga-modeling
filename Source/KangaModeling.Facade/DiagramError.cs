using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KangaModeling.Facade
{
	/// <summary>
	/// Represents an error that occured during the creation of a diagram.
	/// </summary>
	public class DiagramError
	{
		public DiagramError(string message, int tokenLine, int tokenStart, int tokenLength)
		{
			Message = message;
			TokenLine = tokenLine;
			TokenStart = tokenStart;
			TokenLength = tokenLength;
		}

		public string Message { get; private set; }

		public int TokenLine { get; private set; }

		public int TokenStart { get; private set; }

		public int TokenLength { get; private set; }

		public string TokenValue { get; private set; }
	}
}
