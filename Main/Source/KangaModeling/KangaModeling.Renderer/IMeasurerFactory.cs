using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Renderer.Primitives;
using KangaModeling.Graphics.Theming;

namespace KangaModeling.Graphics
{
	public interface IMeasurerFactory
	{
		IMeasurer CreateMeasurer(ITheme theme);
	}
}
