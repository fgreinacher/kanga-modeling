using System.Collections.Generic;
using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using Moq;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class LifelineTest
    {
     
        [Test]
        public void LifelineConstructorTest()
        {
            Matrix matrix = null;
            const string id = "SomeId";
            const string name = "SomeName";
            const int index = 123;
            var target = new Lifeline(matrix, id, name, index);
            Assert.AreEqual(id, target.Id);
            Assert.AreEqual(name, target.Name);
            Assert.AreEqual(index, target.Index);
        }

        [Test]
        [Ignore("TODO Demeter violation inside Pins implementation must be eliminated first.")]
        public void PinsTest()
        {
            var matrixStub = new Mock<Matrix>(MockBehavior.Loose);
            const string id = "SomeId";
            const string name = "SomeName";
            const int index = 123;

            var pinStub = new Mock<RegularPin>();
            
            var rowMock = new Mock<Row>(MockBehavior.Strict);
            rowMock.Setup(row => row[index]).Returns(pinStub.Object);
            //matrixStub.Setup(matrix => matrix.Rows).Returns(Enumerable.Repeat(rowMock.Object, 10).ToArray());

            var target = new Lifeline(matrixStub.Object, id, name, index); 
            IEnumerable<IPin> actual = target.Pins;
            Assert.Inconclusive("Get rid of demeter violation inside the method.");
        }

        [Test]
        public void StateTest()
        {
            var target = new Lifeline(null, null, null, 0);
            LifelineState actual = target.State; 
            Assert.IsNotNull(actual);
        }
    }
}