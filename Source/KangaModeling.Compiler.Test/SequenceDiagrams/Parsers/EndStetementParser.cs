using System;
using System.Collections.Generic;
using System.Linq;
using KangaModeling.Compiler.Test.SequenceDiagrams.Helper;
using KangaModeling.Compiler.SequenceDiagrams;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.Parsers
{
    [TestFixture]
    class EndStetementParser
    {
        [TestCase("end Some unexpected argument", typeof(UnexpectedArgumentStatement))]
        [TestCase("end", typeof(EndStatement))]
        public void ParseTest(string input, Type expectedStatementType)
        {
            var target = new EndStatementParser();
            var actual = target.Parse(input).First();
            Assert.IsInstanceOf(expectedStatementType, actual);
        }

        [TestCase("end Some unexpected argument", new[] { "Some unexpected argument" })]
        [TestCase("end", new string[] {})]
        public void EnsureTokensSimple(string input, string[] arguments)
        {
            var target = new EndStatementParser();
            var actual = target.Parse(input);
            var argumentValues = actual.First().Tokens().Select(token => token.Value);
            var expected = new[] { "end" }.Concat(arguments);
            CollectionAssert.AreEquivalent(expected, argumentValues);
        }
    }
}
