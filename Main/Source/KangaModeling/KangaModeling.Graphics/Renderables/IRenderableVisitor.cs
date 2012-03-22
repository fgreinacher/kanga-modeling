using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Graphics.Renderables;

namespace KangaModeling.Renderer.Renderables
{
	public interface IRenderableVisitor
	{
		void Visit(Renderable renderable);

		void Visit(RenderableText renderableText);

		void Visit(RenderableRectangle renderableRectangle);
	}
}
