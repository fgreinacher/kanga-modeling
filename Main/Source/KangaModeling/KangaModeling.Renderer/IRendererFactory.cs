using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Graphics.Theming;

namespace KangaModeling.Graphics
{
	public interface IRendererFactory
	{
		IRenderer CreateRenderer(ITheme theme);
	}
}
