using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class ReturnTest
    {

        [Test]
        public void ReturnConstructorTest()
        {
            string name = string.Empty; 
            var target = new Return(name);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        public void SignalTypeTest()
        {
            string name = string.Empty; 
            var target = new Return(name); 
            SignalType actual;
            actual = target.SignalType;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}