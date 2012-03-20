namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class StatementParserFactory
    {
        internal virtual StatementParser GetStatementParser(string keyword)
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
