using KangaModeling.Compiler.SequenceDiagrams.Parsing;

namespace KangaModeling.Compiler.SequenceDiagrams.Ast
{
    internal class StatementFactory
    {
        internal StatementParser GetStatementParser(string keyword)
        {
            switch (keyword)
            {
                case "title" :
                    return new TitleStatementParser();
                
                default:
                    return new UnknownStatementParser();
            }
        }
    }
}
