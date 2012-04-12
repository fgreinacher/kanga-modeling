using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
	internal class LifelineNameCell : Cell
	{
		#region Construction / Destruction / Initialisation

		public LifelineNameCell(Grid grid, int row, int column)
			: base(grid, row, column)
		{
		}

		#endregion

		#region Properties

		public string Name { get; set; }

		public bool IsTop { get; set; }

		public bool IsBottom { get; set; }

		public bool IsLeft { get; set; }

		public bool IsRight { get; set; }

		#endregion

		#region Overrides / Overrideables

		protected override void LayoutOutersCore(IGraphicContext graphicContext)
		{
			const float outerMargin = 5;

			if (!IsLeft)
			{
				LeftOuterWidth = outerMargin;
			}

			if (IsFragmentStart)
			{
				LeftOuterWidth += 10;
				TopOuterHeight = 10 + graphicContext.MeasureText(FragmentTitle).Height;
			}

			if (IsFragmentEnd)
			{
				RightOuterWidth += 10;
				BottomOuterHeight += 10;
			}
		}

		protected override void LayoutBodyCore(IGraphicContext graphicContext)
		{
			Size nameSize = graphicContext.MeasureText(Name).Plus(20, 0);

			BodyWidth = nameSize.Width;
			BodyHeight = nameSize.Height;
		}

		protected override void DrawCore(IGraphicContext graphicContext)
		{
			if (IsFragmentStart)
			{
				float width = (FragmentEndCell.X + FragmentEndCell.Width) - X;
				float height = (FragmentEndCell.Y + FragmentEndCell.Height) - Y;

				graphicContext.DrawRectangle(new Point(0, 0), new Size(width, height));

				graphicContext.DrawText(FragmentTitle, HorizontalAlignment.Left,
					 VerticalAlignment.Top, new Point(0, 0), new Size(width, height));
			}

			var location = new Point(
				LeftOuterWidth,
				TopOuterHeight);
			graphicContext.DrawRectangle(location, new Size(BodyWidth, BodyHeight));
			graphicContext.DrawText(Name, HorizontalAlignment.Center, VerticalAlignment.Middle, location, new Size(BodyWidth, BodyHeight));
		}

		#endregion
	}
}