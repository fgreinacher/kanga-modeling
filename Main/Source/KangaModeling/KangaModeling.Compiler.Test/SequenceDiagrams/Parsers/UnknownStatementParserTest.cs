using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams;
using NUnit.Framework;
using System;
using KangaModeling.Compiler.Test.SequenceDiagrams.Helper;
using Type = System.Type;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class UnknownStatementParserTest
    {
        [TestCase("Unknown", typeof(UnknownStatement))]
        [TestCase("Unknown abc", typeof(UnknownStatement))]
        public void ParseTest(string input, Type expectedStatementType)
        {
            UnknownStatementParser target = new UnknownStatementParser();
            var actual = target.Parse(input).First();
            Assert.IsInstanceOf(expectedStatementType, actual);
        }

        [TestCase("Unknown")]
        [TestCase("Unknown abc")]
        [TestCase("Unknown  ")]
        public void EnsureTokens_In_Valid_Statement(string input)
        {
            UnknownStatementParser target = new UnknownStatementParser();
            var actual = target.Parse(input);
            var tokenValues = actual.First().Tokens().Select(token => token.Value);
            var expected = new[] { input };
            CollectionAssert.AreEquivalent(expected, tokenValues);
        }
    }
}
