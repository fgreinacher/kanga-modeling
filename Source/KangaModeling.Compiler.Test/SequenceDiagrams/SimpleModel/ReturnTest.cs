using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.Model;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.ModelComponents
{
    [TestFixture]
    public sealed class ReturnTest : SignalTest
    {
        internal override Signal CreateSignal()
        {
            return new Return(string.Empty);
        }

        [TestCase("")]
        [TestCase("SomeName")]
        public void ReturnConstructorTest(string name)
        {
            var target = new Return(name);
            Assert.AreEqual(name, target.Name);
        }


        [Test]
        public void SignalTypeTest()
        {
            var target = CreateSignal();
            SignalType actual = target.SignalType;
            Assert.AreEqual(SignalType.Return, actual);
        }
    }
}