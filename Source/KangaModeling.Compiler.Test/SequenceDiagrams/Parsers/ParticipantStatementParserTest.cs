using System;
using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams;
using NUnit.Framework;
using KangaModeling.Compiler.Test.SequenceDiagrams.Helper;
using Type = System.Type;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.Parsers
{
    [TestFixture]
    public class ParticipantStatementParserTest
    {
        [TestCase("participant A", typeof(SimpleParticipantStatement))]
        [TestCase("participant", typeof(MissingArgumentStatement))]
        [TestCase("participant Name as A", typeof(ParticipantStatement))]
        [TestCase("participant Name as", typeof(MissingArgumentStatement))]
        public void ParseTest(string input, Type expectedStatementType)
        {
            var target = new ParticipantStatementParser();
            var actual = target.Parse(input).First();
            Assert.IsInstanceOf(expectedStatementType, actual);
        }

        [TestCase("participant A", new[] {"A", "A"})]
        [TestCase("participant", new [] {""})]
        public void EnsureTokensSimple(string input, string[] nameValues)
        {
            ParticipantStatementParser target = new ParticipantStatementParser();
            var actual = target.Parse(input);
            var tokenValues = actual.First().Tokens().Select(token => token.Value);
            var expected = new[] {"participant"}.Concat(nameValues);
            CollectionAssert.AreEquivalent(expected, tokenValues);
        }

        [TestCase("participant Name as A", new[] {"A", "Name"})]
        [TestCase("participant Name as", new[] {""})]
        public void EnsureTokensExtended(string input, string[] tokens)
        {
            ParticipantStatementParser target = new ParticipantStatementParser();
            var actual = target.Parse(input);
            var tokenValues = actual.First().Tokens().Select(token => token.Value);
            var expected = new[] { ParticipantStatementParser.Keyword} .Concat(tokens) ;
            CollectionAssert.AreEquivalent(expected, tokenValues);
        }
    }
}