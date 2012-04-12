using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
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
        public void PinsTest()
        {
            //matrixMoc
            Matrix matrix = null;
            const string id = "SomeId";
            const string name = "SomeName";
            const int index = 123;
            var target = new Lifeline(matrix, id, name, index); 
            IEnumerable<IPin> actual;
            actual = target.Pins;
        }

        [Test]
        public void StateTest()
        {
            //PrivateObject param0 = null; 
            //var target = new Lifeline_Accessor(param0); 
            //LifelineState_Accessor expected = null; 
            //LifelineState_Accessor actual;
            //target.State = expected;
            //actual = target.State;
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}