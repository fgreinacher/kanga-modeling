using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Renderer.Theming;

namespace KangaModeling.Renderer
{
	public interface IEngineFactory
	{
		IMeasurementEngine CreateMeasurementEngine(ITheme theme);

		IRenderingEngine CreateRenderingEngine(ITheme theme);
	}
}
