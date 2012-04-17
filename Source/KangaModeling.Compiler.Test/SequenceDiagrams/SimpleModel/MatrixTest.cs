using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class MatrixTest
    {

        [Test]
        public void MatrixConstructorTest()
        {
            var root = new RootFragment();
            var target = new Matrix(root);
            Assert.NotNull(target.Lifelines);
            Assert.IsInstanceOf(typeof(LifelineCollection), target.Lifelines);
            Assert.AreEqual(0, target.Lifelines.Count);
            
            Assert.NotNull(target.Rows);
            Assert.IsInstanceOf(typeof(RowsCollection), target.Rows);
            Assert.AreEqual(1, target.Rows.Count);
            Assert.IsInstanceOf(typeof(IEnumerable<Pin>), target.Rows[0]);
            Assert.AreEqual(0, target.Rows[0].Count);

            Assert.AreEqual(root, target.Root);
            Assert.IsInstanceOf(typeof(RootFragment), target.Root);
        }


        [Test]
        public void CreateLifelineTest()
        {
            var target = new Matrix(null); 
            const string id = "SomeId"; 
            const string name = "SomeName"; 
            Lifeline actual = target.CreateLifeline(id, name);
            Assert.AreEqual(id, actual.Id);
            Assert.AreEqual(name, actual.Name);
            //TODO Unable to assert matrix. The same Demeter violation.
            //Assert.AreEqual(target, actual.);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void CreateRowTest(int lifelineCount)
        {
            var target = new Matrix(null);
            for (int i = 0; i < lifelineCount; i++)
            {
                string id = i.ToString();
                target.CreateLifeline(id, id);
            }

            Row actual = target.CreateRow();
            Assert.AreEqual(2, target.Rows.Count);
            Assert.AreEqual(actual, target.Rows[1]);

            Assert.AreEqual(lifelineCount, target.Rows[0].Count);
            Assert.AreEqual(lifelineCount, target.Rows[1].Count);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void LastRowTest(int aditionalRowCount)
        {
            var target = new Matrix(null);
            Row expected = target.Rows[0];
            for (int i = 0; i < aditionalRowCount; i++)
            {
                expected = target.CreateRow();
            }
            Row actual = target.LastRow;
            Assert.AreEqual(expected, actual);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void LifelineCountTest(int expected)
        {
            var target = new Matrix(null);
            for (int i = 0; i < expected; i++)
            {
                string id = i.ToString();
                target.CreateLifeline(id, id);
            }

            int actual = target.LifelineCount;
            Assert.AreEqual(expected, actual);
        }
    }
}