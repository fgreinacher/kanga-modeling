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
                case SignalStatementParser.CreateKeyword1:
                case SignalStatementParser.CreateKeyword2:
                    return new SignalStatementParser(keyword);

                case ActivateStatementParser.ActivateKeyword:
                    return new ActivateStatementParser();

                case DeactivateStatementParser.DeactivateKeyword:
                    return new DeactivateStatementParser();

                case OptStatementParser.OptKeyword:
                    return new OptStatementParser();

                case AltStatementParser.AltKeyword:
                    return new AltStatementParser();

                case ElseStatementParser.ElseKeyword:
                    return new ElseStatementParser();

                case LoopStatementParser.LoopKeyword:
                    return new LoopStatementParser();

                case EndStatementParser.EndKeyword:
                    return new EndStatementParser();

                case DisposeStatementParser.DestroyKeyword:
                case DisposeStatementParser.DisposeKeyword:
                    return new DisposeStatementParser();

                default:
                    return new UnknownStatementParser();
            }
        }
    }
}