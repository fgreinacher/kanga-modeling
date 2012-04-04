using System;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

		public Size MeasuredSize
		{
			get;
			private set;
		}

		public Point Location
		{
			get;
			set;
		}

		public bool AutoSize
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

		public float CenterX
		{
			get { return X + Width / 2; }
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

		public Point LocalPointToGlobalPoint(Point localPoint)
		{
			if (Parent == null)
			{
				return localPoint;
			}

			return Parent.GlobalPointToLocalPoint(localPoint.Offset(Location.X, Location.Y));
		}

		public Point GlobalPointToLocalPoint(Point globalPoint)
		{
			if (Parent == null)
			{
				return globalPoint;
			}

			return Parent.GlobalPointToLocalPoint(globalPoint.Offset(-Location.X, -Location.Y));
		}

		public void Layout(IGraphicContext graphicContext)
		{
			foreach (var child in Children)
			{
				child.Layout(graphicContext);
			}

			Arrange(graphicContext);
			Measure(graphicContext);

			if (AutoSize) Size = MeasuredSize;
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

		protected virtual void ArrangeCore(IGraphicContext graphicContext)
		{
		}

		protected virtual Size MeasureCore(IGraphicContext graphicContext)
		{
			float maximumX = 0;
			float maximumY = 0;

			foreach (var child in Children)
			{
				maximumX = Math.Max(child.X + child.Width, maximumX);
				maximumY = Math.Max(child.Y + child.Height, maximumY);
			}

			return new Size(maximumX, maximumY);
		}

		protected virtual void DrawCore(IGraphicContext graphicContext)
		{
		}
		#endregion

		#region Private Methods

		private void Arrange(IGraphicContext graphicContext)
		{
			ArrangeCore(graphicContext);
		}

		private void Measure(IGraphicContext graphicContext)
		{
			MeasuredSize = MeasureCore(graphicContext);
		}

		#endregion
	}
}
