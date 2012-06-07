using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.Model;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.Model
{
    [TestFixture]
    public class RegularPinTest : PinTest
    {
        internal override Pin CreatePin(Row row, Lifeline lifeline)
        {
            return new RegularPin(row, lifeline);
        }

        [Test]
        public void OrientationTest_PinTypeNone()
        {
            var target = CreatePin(); 
            Assert.AreEqual(Orientation.None, target.Orientation);
        }

        [TestCase(0, 1, Orientation.Right)]
        [TestCase(1, 0, Orientation.Left)]
        public void OrientationTest_PinIsEnd(int otherIndex, int targetIndex, Orientation expected)
        {
            var targetLifeLine = new Lifeline(null, null, null, otherIndex);
            var otherLifeline = new Lifeline(null, null, null, targetIndex);
            var target = CreatePin(null, targetLifeLine);
            var other = CreatePin(null, otherLifeline);

            var signal = new Call(null);
            signal.Connect(other, target);

            Assert.AreEqual(expected, target.Orientation);
        }

        [TestCase(0, 1, Orientation.Right)]
        [TestCase(1, 0, Orientation.Left)]
        public void OrientationTest_PinIsStart(int otherIndex, int targetIndex, Orientation expected)
        {
            var targetLifeLine = new Lifeline(null, null, null, otherIndex);
            var otherLifeline = new Lifeline(null, null, null, targetIndex);
            var target = CreatePin(null, targetLifeLine);
            var other = CreatePin(null, otherLifeline);

            var signal = new Call(null);
            signal.Connect(target, other);

            Assert.AreEqual(expected, target.Orientation);
        }
    }
}