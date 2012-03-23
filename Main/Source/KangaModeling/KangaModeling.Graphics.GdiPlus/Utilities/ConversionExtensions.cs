using System;
using System.Drawing;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Graphics.GdiPlus.Utilities
{
    internal static class ConversionExtensions
    {
        public static PointF ToPointF(this Primitives.Point point)
        {
            return new PointF(point.X, point.Y);
        }

        public static SizeF ToSizeF(this Primitives.Size size)
        {
            return new SizeF(size.Width, size.Height);
        }

        public static StringAlignment ToStringAlignment(this HorizontalAlignment horizontalAlignment)
        {
            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    return StringAlignment.Near;

                case HorizontalAlignment.Right:
                    return StringAlignment.Far;

                case HorizontalAlignment.Center:
                    return StringAlignment.Center;

                default:
                    throw new ArgumentOutOfRangeException("horizontalAlignment");
            }
        }


        public static StringAlignment ToStringAlignment(this VerticalAlignment verticalAlignment)
        {
            switch (verticalAlignment)
            {
                case VerticalAlignment.Top:
                    return StringAlignment.Near;

                case VerticalAlignment.Bottom:
                    return StringAlignment.Far;

                case VerticalAlignment.Center:
                    return StringAlignment.Center;

                default:
                    throw new ArgumentOutOfRangeException("verticalAlignment");
            }
        }
    }

}
