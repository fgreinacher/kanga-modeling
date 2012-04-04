using System.Collections.Generic;
using System.Linq;

namespace KangaModeling.Compiler.SequenceDiagrams._Ast
{
    internal static class IEnumerableExtensions
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T element)
        {
            return source.Concat(Enumerable.Repeat(element, 1));
        }

    }
}
