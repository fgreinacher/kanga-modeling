using KangaModeling.Compiler.ClassDiagrams;
using KangaModeling.Compiler.ClassDiagrams.Model;

namespace KangaModeling.Compiler.Test.ClassDiagrams
{
    /// <summary>
    /// Some extension on ClassDiagramTokenStream.
    /// </summary>
    static class ClassDiagramTokenStreamExtesnsions
    {
        internal static IClassDiagram ParseClassDiagram(this ClassDiagramTokenStream classDiagramTokenStream, IParseErrorHandler parseErrorHandler = null)
        {
            return new ClassDiagramParser(classDiagramTokenStream, parseErrorHandler).ParseClassDiagram();
        }
    }
}
