using System;
using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;
using NUnit.Framework;
using KangaModeling.Compiler.Test.SequenceDiagrams.Helper;
using System.Linq;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class TitleStatementParserTest
    {
        [TestCase("title", typeof(MissingArgumentStatement))]
        [TestCase("title abc", typeof(TitleStatement))]
        public void ParseTest(string input, Type expectedStatementType)
        {
            TitleStatementParser target = new TitleStatementParser();
            var actual = target.Parse(input).First();
            Assert.IsInstanceOf(expectedStatementType, actual);
        }

        [TestCase("title", "")]
        [TestCase("title abc", "abc")]
        [TestCase("title  ", "")]
        public void EnsureTokens(string input, string argumentValue)
        {
            TitleStatementParser target = new TitleStatementParser();
            IEnumerable<Statement> actual = target.Parse(input);
            var tokenValues = actual.First().Tokens().Select(token => token.Value);
            var expected = new[] {TitleStatementParser.Keyword, argumentValue};
            CollectionAssert.AreEquivalent(expected, tokenValues);        
        }
    }
}
