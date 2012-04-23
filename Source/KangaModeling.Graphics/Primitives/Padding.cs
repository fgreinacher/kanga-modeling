using System;
using System.Diagnostics;

namespace KangaModeling.Graphics.Primitives
{
    /// <summary>
    /// Represents a width and height in two-dimensional space.
    /// </summary>
    [DebuggerDisplay("Padding (Left={Left} Right={Right} Top={Top} Bottom={Bottom})")]
	public sealed class Padding : IEquatable<Padding>
    {
        public float Left { get; private set; }
        public float Right { get; private set; }
        public float Top { get; private set; }
        public float Bottom { get; private set; }

        public Padding(float padding) 
            : this(padding, padding, padding, padding)
        {
            
        }

        public Padding(float left, float right, float top, float bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public static Size operator +(Size size, Padding padding)
        {
            return new Size(size.Width + padding.Left + padding.Right , size.Height + padding.Top + padding.Bottom);
        }

        public static Padding operator *(Padding padding, float factor)
        {
            return new Padding(padding.Left * factor, padding.Right * factor, padding.Top * factor, padding.Bottom * factor);
        }

        public bool Equals(Padding other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Left.Equals(Left) && other.Right.Equals(Right) && other.Top.Equals(Top) && other.Bottom.Equals(Bottom);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Padding)) return false;
            return Equals((Padding) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Left.GetHashCode();
                result = (result*397) ^ Right.GetHashCode();
                result = (result*397) ^ Top.GetHashCode();
                result = (result*397) ^ Bottom.GetHashCode();
                return result;
            }
        }
    }
}
