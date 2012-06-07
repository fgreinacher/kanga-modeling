using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.Model;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.Model
{
    [TestFixture]
    public sealed class CallTest : SignalTest
    {
        internal override Signal CreateSignal()
        {
            return new Call(string.Empty);
        }


        [TestCase("")]
        [TestCase("SomeName")]
        public void CallConstructorTest(string name)
        {
            var target = new Call(name);
            Assert.AreEqual(name, target.Name);
        }

 
        [Test]
        public void SignalTypeTest()
        {
            var target = CreateSignal(); 
            SignalType actual = target.SignalType;
            Assert.AreEqual(SignalType.Call, actual);
        }
    }
}