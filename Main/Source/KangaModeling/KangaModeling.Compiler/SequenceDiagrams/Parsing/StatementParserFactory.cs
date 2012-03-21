namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class StatementParserFactory
    {
        internal virtual StatementParser GetStatementParser(string keyword)
        {
            switch (keyword)
            {
                case TitleStatementParser.Keyword:
                    return new TitleStatementParser();

                case ParticipantStatementParser.Keyword:
                    return new ParticipantStatementParser();

                case SignalStatementParser.CallKeyword:
                case SignalStatementParser.ReturnKeyword:
                case SignalStatementParser.BackCallKeyword:
                case SignalStatementParser.BackReturnKeyword:
                    return new SignalStatementParser(keyword);

                default:
                    return new UnknownStatementParser();
            }
        }
    }
}
