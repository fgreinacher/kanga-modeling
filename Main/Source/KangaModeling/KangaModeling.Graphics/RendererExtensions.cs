using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Graphics.Renderables;

namespace KangaModeling.Graphics
{
	public static class RendererExtensions
	{
		public static void Render(this IRenderer renderer, IEnumerable<Renderable> renderables)
		{
			foreach (var renderable in renderables)
			{
				var renderableText = renderable as RenderableText;
				if (renderableText != null)
				{
					renderer.RenderText(renderableText);
				}

				var renderableRectangle = renderable as RenderableRectangle;
				if (renderableRectangle != null)
				{
					renderer.RenderRectangle(renderableRectangle);
				}
			}
		}
	}
}
