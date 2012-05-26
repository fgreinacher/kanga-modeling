using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.ClassDiagrams
{
    public static class SizeExtensions
    {
        public static Size Add(this Size s, float x, float y)
        {
            return new Size(s.Width + x, s.Height + y);
        }
    }
}