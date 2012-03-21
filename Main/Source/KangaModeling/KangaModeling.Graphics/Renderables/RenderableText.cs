using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Renderer.Primitives;

namespace KangaModeling.Graphics.Renderables
{
	public sealed class RenderableText : Renderable
	{
		public RenderableText(string text, Point location, Size size)
		{
			Text = text;
			Location = location;
			Size = size;
		}
		
		public Point Location { get; private set; }

		public Size Size { get; private set; }

		public string Text { get; private set; }
	}
}
