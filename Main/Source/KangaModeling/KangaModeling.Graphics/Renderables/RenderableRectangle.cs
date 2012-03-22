using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Renderer.Primitives;
using KangaModeling.Renderer.Renderables;

namespace KangaModeling.Graphics.Renderables
{
	public class RenderableRectangle : Renderable
	{
		public RenderableRectangle(Point location, Size size, int layer = 0)
			: base(location, size, layer)
		{
		}

		public override void Accept(IRenderableVisitor renderableVisitor)
		{
			renderableVisitor.Visit(this);
		}
	}
}
