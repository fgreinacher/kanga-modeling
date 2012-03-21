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

		public int FontSize
		{
			get { return 10; }
		}
	}
}
