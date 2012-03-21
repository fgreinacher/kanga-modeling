using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Renderer.Primitives;

namespace KangaModeling.Graphics
{
	public interface IMeasurer
	{
		Size MeasureText(string text);
	}
}
