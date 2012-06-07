using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.Model;
using Moq;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.ModelComponents
{
    [TestFixture]
    public class RowsCollectionTest
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void ExtendTest(int rowCount)
        {
            var target = new RowsCollection();

            var lifeLineStub = new Mock<Lifeline>(MockBehavior.Loose, null, null, null, 0);
            Lifeline lifeline = lifeLineStub.Object;
            var rowMocks = new Stack<Mock>();
            for (int i = 0; i < rowCount; i++)
            {
                var rowMock = new Mock<Row>(MockBehavior.Strict, 0);
                rowMocks.Push(rowMock);
                rowMock.Setup(row => row.Extend(lifeline));
                Row targetRow = rowMock.Object;
                target.Add(targetRow);
            }
            target.Extend(lifeline);

            foreach (var rowMock in rowMocks)
            {
                rowMock.VerifyAll();
            }
        }
    }
}