using System;
using System.Collections.Generic;
using System.Linq;
using KangaModeling.Compiler.Test.SequenceDiagrams.Helper;
using KangaModeling.Compiler.SequenceDiagrams;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.Parsers
{
    [TestFixture]
    class OptStatementParserTests
    {
        [TestCase("opt Some Guard Expression", typeof(OptStatement))]
        [TestCase("opt", typeof(MissingArgumentStatement))]
        public void ParseTest(string input, Type expectedStatementType)
        {
            var target = new OptStatementParser();
            var actual = target.Parse(input).First();
            Assert.IsInstanceOf(expectedStatementType, actual);
        }

        [TestCase("opt Some Guard Expression", new[] { "Some Guard Expression" })]
        [TestCase("opt A", new[] { "A" })]
        [TestCase("opt", new[] { "" })]
        public void EnsureTokensSimple(string input, string[] arguments)
        {
            OptStatementParser target = new OptStatementParser();
            var actual = target.Parse(input);
            var argumentValues = actual.First().Tokens().Select(token => token.Value);
            var expected = new[] { "opt" }.Concat(arguments);
            CollectionAssert.AreEquivalent(expected, argumentValues);
        }
    }
}
