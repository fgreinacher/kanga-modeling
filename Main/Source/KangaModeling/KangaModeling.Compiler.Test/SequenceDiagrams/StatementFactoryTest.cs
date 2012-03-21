using System;
using KangaModeling.Compiler.SequenceDiagrams;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class StatementFactoryTest
    {
        [TestCase(null, typeof(UnknownStatementParser))]
        [TestCase("", typeof(UnknownStatementParser))]
        [TestCase("abd", typeof(UnknownStatementParser))]
        [TestCase("title", typeof(TitleStatementParser))]
        [TestCase("participant", typeof(ParticipantStatementParser))]
        [TestCase("->", typeof(SignalStatementParser))]
        [TestCase("-->", typeof(SignalStatementParser))]
        [TestCase("<-", typeof(SignalStatementParser))]
        [TestCase("<-", typeof(SignalStatementParser))]
        [TestCase("activate", typeof(ActivateStatementParser))]
        [TestCase("deactivate", typeof(DeactivateStatementParser))]
        public void GetStatementParserTest(string keyword, Type expectedType)
        {
            var target = new StatementParserFactory();
            var actual = target.GetStatementParser(keyword);
            Assert.AreEqual(expectedType, actual.GetType());
        }
    }
}
