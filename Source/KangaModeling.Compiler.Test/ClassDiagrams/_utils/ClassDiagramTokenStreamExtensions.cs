using KangaModeling.Compiler.ClassDiagrams;
using KangaModeling.Compiler.ClassDiagrams.Model;

namespace KangaModeling.Compiler.Test.ClassDiagrams
{
    /// <summary>
    /// Some extension on ClassDiagramTokenStream.
    /// </summary>
    static class ClassDiagramTokenStreamExtesnsions
    {
        internal static IClassDiagram ParseClassDiagram(this ClassDiagramTokenStream classDiagramTokenStream, ClassDiagramParser.ErrorCallback errorCallback = null)
        {
            return new ClassDiagramParser(classDiagramTokenStream, errorCallback).ParseClassDiagram();
        }
    }
}
