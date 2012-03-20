using KangaModeling.Compiler.SequenceDiagrams._Ast;
using KangaModeling.Compiler.SequenceDiagrams._Scanner;

namespace KangaModeling.Compiler.SequenceDiagrams._Parsing
{
    internal abstract class StatementParser
    {
        public abstract Statement Parse(Scanner scanner);
    }
}