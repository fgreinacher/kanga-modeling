using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Renderer.Primitives;
using KangaModeling.Renderer.Renderables;

namespace KangaModeling.Graphics.Renderables
{
	public sealed class RenderableText : Renderable
	{
		public RenderableText(string text, Point location, Size size, int layer = 0)
			: base(location, size, layer)
		{
			Text = text;
		}

		public string Text { get; private set; }

		public override void Accept(IRenderableVisitor renderableVisitor)
		{
			renderableVisitor.Visit(this);
		}
	}
}
