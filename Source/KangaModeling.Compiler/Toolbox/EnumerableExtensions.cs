using System;
using System.Collections.Generic;
using System.Linq;

namespace KangaModeling.Compiler.Toolbox
{

    /// <summary>
    /// Some additions to IEnumerable.
    /// </summary>
    public static class EnumerableExtensions
    {

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
                action(item);
        }

    }
}