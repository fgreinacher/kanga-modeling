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
        public void CreateLifelineTest()
        {
            var target = new Matrix(); 
            string id = string.Empty; 
            string name = string.Empty; 
            Lifeline expected = null; 
            Lifeline actual;
            actual = target.CreateLifeline(id, name);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void CreateRowTest()
        {
            var target = new Matrix(); 
            Row expected = null; 
            Row actual;
            actual = target.CreateRow();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void ItemTest()
        {
            var target = new Matrix(); 
            int lifelineIndex = 0; 
            int rowIndex = 0; 
            Pin actual;
            actual = target[lifelineIndex, rowIndex];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void LastRowTest()
        {
            var target = new Matrix(); 
            Row actual;
            actual = target.LastRow;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void LifelineCountTest()
        {
            var target = new Matrix(); 
            int actual;
            actual = target.LifelineCount;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        
        public void LifelinesTest()
        {
            ISequenceDiagram target = new Matrix(); 
            IEnumerable<ILifeline> actual;
            actual = target.Lifelines;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        
        public void LifelinesTest1()
        {
            //var target = new Matrix_Accessor(); 
            //LifelineCollection_Accessor expected = null; 
            //LifelineCollection_Accessor actual;
            //target.Lifelines = expected;
            //actual = target.Lifelines;
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void MatrixConstructorTest()
        {
            var target = new Matrix();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        
        public void RootTest()
        {
            ISequenceDiagram target = new Matrix(); 
            IFragment actual;
            actual = target.Root;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void RootTest1()
        {
            var target = new Matrix(); 
            RootFragment actual;
            actual = target.Root;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void RowCountTest()
        {
            var target = new Matrix(); 
            int actual;
            actual = target.RowCount;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        
        public void RowsTest()
        {
            //var target = new Matrix_Accessor(); 
            //RowsCollection_Accessor expected = null; 
            //RowsCollection_Accessor actual;
            //target.Rows = expected;
            //actual = target.Rows;
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}