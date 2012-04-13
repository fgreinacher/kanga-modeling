using System;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class OpenPinTest : PinTest
    {

        internal override Pin CreatePin(Row row, Lifeline lifeline)
        {
            return new OpenPin(lifeline, Orientation.None, new Token());
        }

        [TestCase(Orientation.None)]
        [TestCase(Orientation.Left)]
        [TestCase(Orientation.Right)]
        public void OpenPinConstructorTest(Orientation orientation)
        {
            Lifeline lifeline = null; 
            var token = new Token(); 
            var target = new OpenPin(lifeline, orientation, token);
            Assert.AreEqual(orientation, target.Orientation);
            Assert.AreEqual(lifeline, target.Lifeline);
            Assert.AreEqual(token, target.Token);
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public override void RowTest()
        {
            var target = CreatePin();
            Assert.IsNull(target.Row);
        }

        [TestCase(0)]
        [ExpectedException(typeof(NotSupportedException))]
        public override void RowIndexTest(int index)
        {
            var target = CreatePin();
            Assert.Equals(int.MaxValue, target.RowIndex);
        }
    }
}