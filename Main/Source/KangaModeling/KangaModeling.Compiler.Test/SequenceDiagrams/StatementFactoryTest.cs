using System;
using KangaModeling.Compiler.SequenceDiagrams.Ast;
using NUnit.Framework;
using KangaModeling.Compiler.SequenceDiagrams.Parsing;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class StatementFactoryTest
    {
        [TestCase(null, typeof(UnknownStatementParser))]
        [TestCase("", typeof(UnknownStatementParser))]
        [TestCase("abd", typeof(UnknownStatementParser))]
        [TestCase("title", typeof(TitleStatementParser))]
        public void GetStatementParserTest(string keyword, Type expectedType)
        {
            var target = new StatementFactory();
            var actual = target.GetStatementParser(keyword);
            Assert.AreEqual(expectedType, actual.GetType());
        }
    }
}
