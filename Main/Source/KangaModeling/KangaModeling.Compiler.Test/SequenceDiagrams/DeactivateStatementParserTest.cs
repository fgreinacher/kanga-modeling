using System;
using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;
using NUnit.Framework;
using KangaModeling.Compiler.Test.SequenceDiagrams.Helper;
using System.Linq;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class DeactivateStatementParserTest
    {
        [TestCase("deactivate", typeof(MissingArgumentStatement))]
        [TestCase("deactivate A", typeof(DeactivateStatement))]
        public void ParseTest(string input, Type expectedStatementType)
        {
            DeactivateStatementParser target = new DeactivateStatementParser();
            var actual = target.Parse(input).First();
            Assert.IsInstanceOf(expectedStatementType, actual);
        }

        [TestCase("deactivate", new[]{"deactivate", ""})]
        [TestCase("deactivate A", new[] { "deactivate", "A" })]
        [TestCase("deactivate ABCD ABCD", new[] { "deactivate", "ABCD ABCD" })]
        public void EnsureTokens(string input, string[] expectedTokenValues)
        {
            DeactivateStatementParser target = new DeactivateStatementParser();
            IEnumerable<Statement> actual = target.Parse(input);
            var actualTokenValues = actual.SelectMany(st=>st.Tokens()).Select(token => token.Value);
            CollectionAssert.AreEquivalent(actualTokenValues, actualTokenValues);        
        }
    }
}
