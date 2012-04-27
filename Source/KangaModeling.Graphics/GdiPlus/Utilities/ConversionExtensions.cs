using System;
using System.Drawing;

namespace KangaModeling.Graphics.GdiPlus.Utilities
{
    internal static class ConversionExtensions
    {
		public static Color ToColor(this Primitives.Color color)
		{
			return Color.FromArgb(color.A, color.R, color.G, color.B);
		}

        public static PointF ToPointF(this Primitives.Point point)
        {
            return new PointF(point.X, point.Y);
        }

        public static SizeF ToSizeF(this Primitives.Size size)
        {
            return new SizeF(size.Width, size.Height);
        }

		public static StringAlignment ToStringAlignment(this Primitives.HorizontalAlignment horizontalAlignment)
        {
            switch (horizontalAlignment)
            {
				case Primitives.HorizontalAlignment.Left:
                    return StringAlignment.Near;

				case Primitives.HorizontalAlignment.Right:
                    return StringAlignment.Far;

				case Primitives.HorizontalAlignment.Center:
                    return StringAlignment.Center;

                default:
                    throw new ArgumentOutOfRangeException("horizontalAlignment");
            }
        }


		public static StringAlignment ToStringAlignment(this Primitives.VerticalAlignment verticalAlignment)
        {
            switch (verticalAlignment)
            {
				case Primitives.VerticalAlignment.Top:
                    return StringAlignment.Near;

				case Primitives.VerticalAlignment.Bottom:
                    return StringAlignment.Far;

				case Primitives.VerticalAlignment.Middle:
                    return StringAlignment.Center;

                default:
                    throw new ArgumentOutOfRangeException("verticalAlignment");
            }
        }
    }

}
