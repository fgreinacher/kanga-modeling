using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal abstract class StatementParser
    {
        public abstract IEnumerable<Statement> Parse(Scanner scanner);
    }
}