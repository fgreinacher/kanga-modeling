using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Graphics.Theming
{
	public interface ITheme
	{
		string Font { get; }

		float FontSize { get; }

		float Padding { get; }
	}
}
