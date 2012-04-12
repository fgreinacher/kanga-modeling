using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class RegularPinTest
    {
      
        [Test]
        public void OrientationTest()
        {
            Row row = null; 
            Lifeline lifeline = null; 
            var target = new RegularPin(row, lifeline); 
            Orientation actual;
            actual = target.Orientation;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void OtherEndTest()
        {
            //PrivateObject param0 = null; 
            //var target = new RegularPin_Accessor(param0); 
            //IPin expected = null; 
            //IPin actual;
            //actual = target.OtherEnd();
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void RegularPinConstructorTest()
        {
            Row row = null; 
            Lifeline lifeline = null; 
            var target = new RegularPin(row, lifeline);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}