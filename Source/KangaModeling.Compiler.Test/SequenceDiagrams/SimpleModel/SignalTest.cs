using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class SignalTest
    {
        internal virtual Signal CreateSignal()
        {
            // TODO: Instantiate an appropriate concrete class.
            Signal target = null;
            return target;
        }

        [Test]
        public void ConnectTest()
        {
            Signal target = CreateSignal(); 
            Pin start = null; 
            Pin end = null; 
            target.Connect(start, end);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void EndTest()
        {
            Signal target = CreateSignal(); 
            IPin actual;
            actual = target.End;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void NameTest()
        {
            Signal target = CreateSignal(); 
            string expected = string.Empty; 
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void RowIndexTest()
        {
            Signal target = CreateSignal(); 
            int actual;
            actual = target.RowIndex;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void SignalTypeTest()
        {
            Signal target = CreateSignal(); 
            SignalType actual;
            actual = target.SignalType;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void StartTest()
        {
            Signal target = CreateSignal(); 
            IPin actual;
            actual = target.Start;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}