﻿using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class SignalStatementParser : StatementParser
    {
        public const string CallKeyword = "->";
        public const string BackCallKeyword = "<-";
        public const string ReturnKeyword = "-->";
        public const string BackReturnKeyword = "<--";
        public const string ColonKeyowrd = ":";

        private readonly string m_Signal;

        public SignalStatementParser()
            : this(null)
        {}

        public SignalStatementParser(string signal)
        {
            m_Signal = signal;
        }

        public string GetSignalKeyword(Scanner scanner)
        {
            return m_Signal ?? scanner.GetKeyWord();
        }

        public override IEnumerable<Statement> Parse(Scanner scanner)
        {
            Token source = scanner.ReadTo(GetSignalKeyword(scanner));
            Token signalKeyword = scanner.ReadSignal();

            yield return
                !source.IsEmpty()
                    ? (Statement)new FindOrCreateParticipantStatement(source)
                    : (Statement)new MissingArgumentStatement(signalKeyword, source);

            Token target = scanner.ReadTo(ColonKeyowrd);
            yield return
                !target.IsEmpty()
                    ? (Statement)new FindOrCreateParticipantStatement(target)
                    : (Statement)new MissingArgumentStatement(signalKeyword, target);

            scanner.SkipWhile(ch=>!char.IsLetterOrDigit(ch));
            Token signalName = scanner.ReadToEnd();

            switch (signalKeyword.Value)
            {
                case CallKeyword:
                    yield return new CallSignalStatement(signalKeyword, source, target, signalName);
                    break;

                case BackCallKeyword:
                    yield return new CallSignalStatement(signalKeyword, target, source, signalName);
                    break;

                case ReturnKeyword:
                    yield return new ReturnSignalStatement(signalKeyword, source, target, signalName);
                    break;

                case BackReturnKeyword:
                    yield return new ReturnSignalStatement(signalKeyword, target, source, signalName);
                    break;

                default:
                    yield return new UnknownStatement(signalKeyword);
                    break;
            }
        }
    }
}