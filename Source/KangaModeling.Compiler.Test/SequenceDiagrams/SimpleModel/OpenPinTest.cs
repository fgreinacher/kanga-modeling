using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class OpenPinTest
    {
        [Test]
        public void OpenPinConstructorTest()
        {
            Lifeline lifeline = null; 
            var orientation = new Orientation(); 
            var token = new Token(); 
            var target = new OpenPin(lifeline, orientation, token);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        public void OrientationTest()
        {
            Lifeline lifeline = null; 
            var orientation = new Orientation(); 
            var token = new Token(); 
            var target = new OpenPin(lifeline, orientation, token); 
            Orientation actual;
            actual = target.Orientation;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        
        public void TokenTest()
        {
            //PrivateObject param0 = null; 
            //var target = new OpenPin_Accessor(param0); 
            //var expected = new Token(); 
            //Token actual;
            //target.Token = expected;
            //actual = target.Token;
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}