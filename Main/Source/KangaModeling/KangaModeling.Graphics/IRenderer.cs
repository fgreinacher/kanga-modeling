using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Graphics.Renderables;

namespace KangaModeling.Graphics
{
	public interface IRenderer
	{
		void RenderText(RenderableText renderableText);

		void RenderRectangle(RenderableRectangle renderableRectangle);
	}
}
