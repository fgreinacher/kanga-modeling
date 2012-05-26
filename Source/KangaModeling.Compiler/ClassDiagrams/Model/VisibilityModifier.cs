using System;

namespace KangaModeling.Compiler.ClassDiagrams.Model
{
    public static class VisibilityModifierExtensions
    {
        public static string GetDisplayText(this VisibilityModifier modifier)
        {
            switch (modifier)
            {
                case VisibilityModifier.Public:
                    return "+";
                case VisibilityModifier.Protected:
                    return "#";
                case VisibilityModifier.Private:
                    return "~";
                case VisibilityModifier.Internal:
                    return "-";
            }

            throw new ArgumentException("unexpected visibility modifier: " + modifier.ToString());
        }
    }

    /// <summary>
    /// The visilibity of a member
    /// </summary>
    public enum VisibilityModifier
    {
        Public,
        Internal,
        Private,
        Protected
    }
}