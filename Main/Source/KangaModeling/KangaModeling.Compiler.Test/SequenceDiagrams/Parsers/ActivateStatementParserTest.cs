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
    public class ActivateStatementParserTest
    {
        [TestCase("activate", typeof(MissingArgumentStatement))]
        [TestCase("activate A", typeof(ActivateStatement))]
        public void ParseTest(string input, Type expectedStatementType)
        {
            ActivateStatementParser target = new ActivateStatementParser();
            var actual = target.Parse(input).First();
            Assert.IsInstanceOf(expectedStatementType, actual);
        }

        [TestCase("activate", new[]{"activate", ""})]
        [TestCase("activate A", new[] { "activate", "A" })]
        [TestCase("activate ABCD ABCD", new[] { "activate", "ABCD ABCD" })]
        public void EnsureTokens(string input, string[] expectedTokenValues)
        {
            ActivateStatementParser target = new ActivateStatementParser();
            IEnumerable<Statement> actual = target.Parse(input);
            var actualTokenValues = actual.SelectMany(st=>st.Tokens()).Select(token => token.Value);
            CollectionAssert.AreEquivalent(actualTokenValues, actualTokenValues);        
        }
    }
}
