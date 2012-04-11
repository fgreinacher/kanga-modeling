using System;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System.Collections.Generic;

namespace KangaModeling.Visuals
{
	public class Visual
	{
		#region Fields

		private readonly List<Visual> m_Children = new List<Visual>();
		private Visual m_Parent;

		#endregion

		#region Construction / Destruction / Initialisation

		public Visual()
		{
			Location = Point.Empty;
			Size = Size.Empty;
		}

		#endregion

		#region Properties

		public IEnumerable<Visual> Children
		{
			get { return m_Children; }
		}

		public Visual Parent
		{
			get { return m_Parent; }
		}
        
		public Point Location
		{
			get;
			set;
		}
        
		public Size Size
		{
			get;
			set;
		}

		public float Width
		{
			get { return Size.Width; }
			set { Size = new Size(value, Height); }
		}

		public float Height
		{
			get { return Size.Height; }
			set { Size = new Size(Width, value); }
		}

		public float X
		{
			get { return Location.X; }
			set { Location = new Point(value, Y); }
		}

		public float Y
		{
			get { return Location.Y; }
			set { Location = new Point(X, value); }
		}

		#endregion

		#region Public Methods

		public void AddChild(Visual visual)
		{
			if (visual == null) throw new ArgumentNullException("visual");
			if (visual == this) throw new ArgumentException("The new child must not be the same as the.", "visual");
			if (visual.m_Parent != null) throw new ArgumentException("The new child must not have a parent.", "visual");

			visual.m_Parent = this;
			m_Children.Add(visual);
		}

		public void RemoveChild(Visual visual)
		{
			if (visual == null) throw new ArgumentNullException("visual");
			if (visual == this) throw new ArgumentException("The visual to remove must not be the same as the parent.", "visual");
			if (visual.m_Parent != this) throw new ArgumentException("The parent of the child to remove must be the parent.", "visual");

			visual.m_Parent = null;
			m_Children.Remove(visual);
		}
        
		public void Layout(IGraphicContext graphicContext)
        {
            LayoutCore(graphicContext);
		}

		public void Draw(IGraphicContext graphicContext)
		{
			DrawCore(graphicContext);

			foreach (var child in Children)
			{
				using (graphicContext.ApplyOffset(child.X, child.Y))
				{
					child.Draw(graphicContext);
				}
			}
		}

		#endregion

		#region Overrides / Overrideables

		protected virtual void LayoutCore(IGraphicContext graphicContext)
		{
		}
        
		protected virtual void DrawCore(IGraphicContext graphicContext)
		{
		}

		#endregion
        
    }
}
