using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class RowTest
    {
      
        [Test]
        public void ExtendTest()
        {
            int index = 0; 
            var target = new Row(index); 
            IEnumerable<Lifeline> lifeLine = null; 
            target.Extend(lifeLine);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void ExtendTest1()
        {
            int index = 0; 
            var target = new Row(index); 
            Lifeline lifeLine = null; 
            target.Extend(lifeLine);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        
        public void IndexTest()
        {
            //PrivateObject param0 = null; 
            //var target = new Row_Accessor(param0); 
            //int expected = 0; 
            //int actual;
            //target.Index = expected;
            //actual = target.Index;
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void ItemTest()
        {
            int index = 0; 
            var target = new Row(index); 
            Lifeline lifeline = null; 
            Pin actual;
            actual = target[lifeline];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void RowConstructorTest()
        {
            int index = 0; 
            var target = new Row(index);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}