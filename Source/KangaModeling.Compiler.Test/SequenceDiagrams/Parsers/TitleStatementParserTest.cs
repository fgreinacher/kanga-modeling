using System;
using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;
using NUnit.Framework;
using KangaModeling.Compiler.Test.SequenceDiagrams.Helper;
using System.Linq;
using Type = System.Type;

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

        [TestCase("title", new [] {""})]
        [TestCase("title abc", new[] {"abc"})]
        [TestCase("title  ", new [] {""})]
        public void EnsureTokens(string input, string[] argumentValues)
        {
            TitleStatementParser target = new TitleStatementParser();
            IEnumerable<Statement> actual = target.Parse(input);
            var tokenValues = actual.First().Tokens().Select(token => token.Value);
            var expected = new[] {TitleStatementParser.Keyword}.Concat(argumentValues);
            CollectionAssert.AreEquivalent(expected, tokenValues);        
        }
    }
}
