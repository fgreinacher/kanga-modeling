using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class ParserTest
    {
        [TestCase("statement", 1)]
        [TestCase("statement", 3)]
        [TestCase("statement", 13)]
        public void Parse_N_Statements(string statement, int expectedCount )
        {
            var lines = Enumerable.Repeat(statement, expectedCount);
            var statementMock = new Mock<Statement>();
            var statementParserMock = new Mock<StatementParser>(MockBehavior.Strict);
            var factoryMock = new Mock<StatementParserFactory>();

            using (Scanner scanner = new Scanner(lines))
            {
                IEnumerable<Statement> statements = new [] {statementMock.Object};
                statementParserMock.Setup(statementParser => statementParser.Parse(scanner)).Returns(statements);
                factoryMock.Setup(factory => factory.GetStatementParser(statement)).Returns(statementParserMock.Object);

                Parser target = new Parser(scanner, factoryMock.Object);
                var result = target.Parse();
                Assert.AreEqual(expectedCount, result.Count());
                CollectionAssert.AllItemsAreNotNull(result);
            }
            statementParserMock.VerifyAll();
            factoryMock.VerifyAll();
        }


        [Test]
        public void Parse_Empty()
        {
            Scanner scanner = new Scanner(string.Empty);
            Parser target = new Parser(scanner, null);
            var result = target.Parsers();
            Assert.AreEqual(0, result.Count());
        }

    }
}
