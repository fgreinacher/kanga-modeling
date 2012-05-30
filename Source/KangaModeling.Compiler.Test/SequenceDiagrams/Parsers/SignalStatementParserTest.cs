using System;
using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams;
using NUnit.Framework;
using KangaModeling.Compiler.Test.SequenceDiagrams.Helper;
using Type = System.Type;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.Parsers
{
    [TestFixture]
    public class SignalStatementParserTest
    {
        [TestCase("A->B", new[] { typeof(EnsureParticipantStatement), typeof(EnsureParticipantStatement), typeof(CallSignalStatement) })]
        [TestCase("A-->B", new[] { typeof(EnsureParticipantStatement), typeof(EnsureParticipantStatement), typeof(ReturnSignalStatement) })]
        [TestCase("A<-B", new[] { typeof(EnsureParticipantStatement), typeof(EnsureParticipantStatement), typeof(CallSignalStatement) })]
        [TestCase("A<--B", new[] { typeof(EnsureParticipantStatement), typeof(EnsureParticipantStatement), typeof(ReturnSignalStatement) })]

        [TestCase("->B", new[] { typeof(MissingArgumentStatement), typeof(EnsureParticipantStatement) })]
        [TestCase("-->B", new[] { typeof(MissingArgumentStatement), typeof(EnsureParticipantStatement) })]
        [TestCase("<-B", new[] { typeof(MissingArgumentStatement), typeof(EnsureParticipantStatement) })]
        [TestCase("<--B", new[] { typeof(MissingArgumentStatement), typeof(EnsureParticipantStatement) })]

        [TestCase("A->", new[] { typeof(EnsureParticipantStatement), typeof(MissingArgumentStatement) })]
        [TestCase("A-->", new[] { typeof(EnsureParticipantStatement), typeof(MissingArgumentStatement) })]
        [TestCase("A<-", new[] { typeof(EnsureParticipantStatement), typeof(MissingArgumentStatement) })]
        [TestCase("A<--", new[] { typeof(EnsureParticipantStatement), typeof(MissingArgumentStatement) })]

        [TestCase("A->B : text", new[] { typeof(EnsureParticipantStatement), typeof(EnsureParticipantStatement), typeof(CallSignalStatement) })]
        [TestCase("A-->B : text", new[] { typeof(EnsureParticipantStatement), typeof(EnsureParticipantStatement), typeof(ReturnSignalStatement) })]
        [TestCase("A<-B : text", new[] { typeof(EnsureParticipantStatement), typeof(EnsureParticipantStatement), typeof(CallSignalStatement) })]
        [TestCase("A<--B : text", new[] { typeof(EnsureParticipantStatement), typeof(EnsureParticipantStatement), typeof(ReturnSignalStatement) })]

        [TestCase("A->: text", new[] { typeof(EnsureParticipantStatement), typeof(MissingArgumentStatement) })]
        [TestCase("A-->: text", new[] { typeof(EnsureParticipantStatement), typeof(MissingArgumentStatement) })]
        [TestCase("A<-: text", new[] { typeof(EnsureParticipantStatement), typeof(MissingArgumentStatement) })]
        [TestCase("A<--: text", new[] { typeof(EnsureParticipantStatement), typeof(MissingArgumentStatement) })]

        [TestCase("->B: text", new[] { typeof(MissingArgumentStatement), typeof(EnsureParticipantStatement) })]
        [TestCase("-->B: text", new[] { typeof(MissingArgumentStatement), typeof(EnsureParticipantStatement) })]
        [TestCase("<-B: text", new[] { typeof(MissingArgumentStatement), typeof(EnsureParticipantStatement) })]
        [TestCase("<--B: text", new[] { typeof(MissingArgumentStatement), typeof(EnsureParticipantStatement) })]

        [TestCase("A<-->B: text", new[] { typeof(EnsureParticipantStatement), typeof(EnsureParticipantStatement), typeof(UnknownStatement) })]
        [TestCase("skdjskdj: text", new[] { typeof(MissingArgumentStatement), typeof(MissingArgumentStatement) })]
        [TestCase("----->", new[] { typeof(MissingArgumentStatement), typeof(MissingArgumentStatement) })]
        public void ParseTest(string input, Type[] expectedStatementTypes)
        {
            var target = new SignalStatementParser();
            var actualStatementTypes = target.Parse(input).Select(statement => statement.GetType());
            CollectionAssert.AreEquivalent(expectedStatementTypes, actualStatementTypes);
        }

        [TestCase("A->B", "A", "B")]
        [TestCase(" A->B", "A", "B")]
        [TestCase(" A ->B", "A", "B")]
        [TestCase(" A->B", "A", "B")]
        [TestCase("A-> B", "A", "B")]
        [TestCase("A-> B ", "A", "B")]
        [TestCase("A->B ", "A", "B")]

        [TestCase("A-->B", "A", "B")]

        [TestCase("A<-B", "B", "A")]

        [TestCase("A<--B", "B", "A")]
        public void VerifySourceAndTarget(string input, string expectedSourceValue, string expectedTargetValue)
        {
            var parser = new SignalStatementParser();
            var statements = parser.Parse(input);
            var signalStatement = statements.OfType<SignalStatement>().Single();

            Assert.That(signalStatement.Source.Value, Is.EqualTo(expectedSourceValue));
            Assert.That(signalStatement.Target.Value, Is.EqualTo(expectedTargetValue));
        }

        [Ignore]
        public void EnsureTokensSimple(string input, string[] expectedTokenValues)
        {
            var target = new SignalStatementParser();
            var actual = target.Parse(input);
            var actualTokenvalues = actual.SelectMany(statement => statement.Tokens()).Select(token => token.Value);
            CollectionAssert.AreEquivalent(expectedTokenValues, actualTokenvalues);
        }
    }
}