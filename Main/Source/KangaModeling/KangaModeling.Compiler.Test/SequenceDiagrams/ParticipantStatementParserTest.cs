using System;
using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams;
using NUnit.Framework;
using KangaModeling.Compiler.Test.SequenceDiagrams.Helper;
using Type = System.Type;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class ParticipantStatementParserTest
    {
        [TestCase("participant A", typeof(SimpleParticipantStatement))]
        [TestCase("participant", typeof(MissingArgumentStatement))]
        [TestCase("participant Description as A", typeof(ParticipantStatement))]
        [TestCase("participant Description as", typeof(ParticipantStatement))]
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

        [TestCase("participant Description as A", "A", "Description")]
        [TestCase("participant Description as", "", "Description")]
        public void EnsureTokensExtended(string input, string nameValue, string descriptionValue)
        {
            ParticipantStatementParser target = new ParticipantStatementParser();
            var actual = target.Parse(input);
            var tokenValues = actual.First().Tokens().Select(token => token.Value);
            var expected = new[] { ParticipantStatementParser.Keyword, nameValue, descriptionValue };
            CollectionAssert.AreEquivalent(expected, tokenValues);
        }
    }
}