using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.Test.SequenceDiagrams.Helper;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.Parsers
{
    [TestFixture]
    class ElseStatementParserTest
    {
        [TestCase("else Any condition", typeof(ElseStatement))]
        [TestCase("else", typeof(ElseStatement))]
        public void ParseTest(string input, Type expectedStatementType)
        {
            var target = new ElseStatementParser();
            var actual = target.Parse(input).First();
            Assert.IsInstanceOf(expectedStatementType, actual);
        }

        [TestCase("else Some Guard Expression", new[] { "Some Guard Expression" })]
        [TestCase("else A", new[] { "A" })]
        [TestCase("else", new[] { "" })]
        public void EnsureTokensSimple(string input, string[] arguments)
        {
            var target = new ElseStatementParser();
            var actual = target.Parse(input);
            var argumentValues = actual.First().Tokens().Select(token => token.Value);
            var expected = new[] { "else" }.Concat(arguments);
            CollectionAssert.AreEquivalent(expected, argumentValues);
        }
    }
}
