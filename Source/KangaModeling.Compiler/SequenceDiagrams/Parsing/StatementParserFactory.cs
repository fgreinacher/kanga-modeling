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

                case ActivateStatementParser.ActivateKeyword:
                    return new ActivateStatementParser();

                case DeactivateStatementParser.DeactivateKeyword:
                    return new DeactivateStatementParser();

                case OptStatementParser.OptKeyword:
                    return new OptStatementParser();

                case EndStatementParser.EndKeyword:
                    return new EndStatementParser();

                default:
                    return new UnknownStatementParser();
            }
        }
    }
}
