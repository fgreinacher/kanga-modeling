namespace KangaModeling.Compiler.SequenceDiagrams._Parsing
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
