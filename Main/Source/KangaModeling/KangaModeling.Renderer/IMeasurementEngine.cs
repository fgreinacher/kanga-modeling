using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Renderer.Core;

namespace KangaModeling.Renderer
{
	public interface IMeasurementEngine
	{
		Size MeasureText(string text);
	}
}
