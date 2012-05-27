using System;
using System.Collections.Generic;

namespace KangaModeling.Compiler.Toolbox
{
    public static class HashSetExtensions
    {
        public static void AddRange<T>(this HashSet<T> @this, params T[] items)
        {
            if (@this == null) throw new NullReferenceException("@this");
            items.ForEach(item => @this.Add(item));
        }
    }
}