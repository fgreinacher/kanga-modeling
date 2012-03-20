using KangaModeling.Compiler.SequenceDiagrams.Ast;
using KangaModeling.Compiler.SequenceDiagrams.Reading;

namespace KangaModeling.Compiler.SequenceDiagrams.Parsing
{
    internal abstract class StatementParser
    {
        public abstract Statement Parse(Scanner scanner);
    }
}