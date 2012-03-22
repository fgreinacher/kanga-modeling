using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Graphics.Theming
{
	public sealed class SimpleTheme : ITheme
	{
		public string Font
		{
			get { return "Arial"; }
		}

		public float FontSize
		{
			get { return 10; }
		}

		public float Padding
		{
			get { return 10; }
		}
	}
}
