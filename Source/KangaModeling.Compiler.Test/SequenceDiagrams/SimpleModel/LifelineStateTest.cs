using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class LifelineStateTest
    {
      
        [Test]
        public void LeftLevelTest()
        {
            var target = new LifelineState(); 
            int expected = 0; 
            int actual;
            target.LeftLevel = expected;
            actual = target.LeftLevel;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void LifelineStateConstructorTest()
        {
            var target = new LifelineState();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        
        public void OpenPinsTest()
        {
            //var target = new LifelineState_Accessor(); 
            //Stack<OpenPin_Accessor> expected = null; 
            //Stack<OpenPin_Accessor> actual;
            //target.OpenPins = expected;
            //actual = target.OpenPins;
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void RightLevelTest()
        {
            var target = new LifelineState(); 
            int expected = 0; 
            int actual;
            target.RightLevel = expected;
            actual = target.RightLevel;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}