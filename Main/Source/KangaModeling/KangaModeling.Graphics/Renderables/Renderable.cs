using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Renderer.Primitives;
using KangaModeling.Renderer.Renderables;

namespace KangaModeling.Graphics.Renderables
{
	public abstract class Renderable
	{
		public Renderable(Point location, Size size, int layer)
		{
			Location = location;
			Size = size;
			Layer = layer;
		}

		public Point Location { get; private set; }

		public Size Size { get; private set; }

		public int Layer { get; private set; }

		public virtual void Accept(IRenderableVisitor renderableVisitor)
		{
			renderableVisitor.Visit(this);
		}
	}
}
