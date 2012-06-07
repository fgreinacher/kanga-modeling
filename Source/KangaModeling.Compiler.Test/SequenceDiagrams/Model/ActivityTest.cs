using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.Model;
using Moq;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.ModelComponents
{
    [TestFixture]
    public class ActivityTest
    {
        private Mock<Pin> CreatePinMock(Activity activity, MockBehavior mockBehavior = MockBehavior.Strict)
        {
            var lifelineMock = new Mock<Lifeline>(null, null, null, 0);
            lifelineMock.Setup(lifeline => lifeline.Index).Returns(0);

            var startPinMock = new Mock<Pin>(mockBehavior, null, lifelineMock.Object, PinType.None);
            startPinMock.Setup(pin => pin.Orientation).Returns(Orientation.None);

            if (activity != null)
                startPinMock.Setup(pin => pin.SetActivity(activity));

            return startPinMock;
        }

        [TestCase(0)]
        [TestCase(10)]
        public void ActivityConstructorTest(int level)
        {
            var target = new Activity(level);
            Assert.AreEqual(level, target.Level);
        }

        [TestCase(0)]
        [TestCase(10)]
        public void ConnectTest(int level)
        {
            var target = new Activity(level);

            var startPinMock = CreatePinMock(target);
            var endPinMock = CreatePinMock(target);

            Pin startPin = startPinMock.Object;
            Pin endPin = endPinMock.Object;
            target.Connect(startPin, endPin);

            Assert.AreEqual(startPin, target.Start);
            Assert.AreEqual(endPin, target.End);

            startPinMock.VerifyAll();
            endPinMock.VerifyAll();
        }

        [TestCase(0)]
        [TestCase(12)]
        public void EndRowIndexTest(int rowIndex)
        {
            var target = new Activity(0);

            var startPinStub = CreatePinMock(null, MockBehavior.Loose);

            var endPinStub = CreatePinMock(null, MockBehavior.Loose);
            endPinStub.Setup(pin => pin.RowIndex).Returns(rowIndex);

            target.Connect(startPinStub.Object, endPinStub.Object);

            int actual = target.EndRowIndex;
            Assert.AreEqual(rowIndex, actual);
        }

        [TestCase(0)]
        [TestCase(12)]
        public void StartRowIndexTest(int rowIndex)
        {
            var target = new Activity(0);

            var startPinStub = CreatePinMock(null, MockBehavior.Loose);
            startPinStub.Setup(pin => pin.RowIndex).Returns(rowIndex);

            var endPinStub = CreatePinMock(null, MockBehavior.Loose);

            target.Connect(startPinStub.Object, endPinStub.Object);

            int actual = target.StartRowIndex;
            Assert.AreEqual(rowIndex, actual);
        }

        [Test]
        public void LifelineTest()
        {
            var target = new Activity(0);

            Pin endPin = CreatePinMock(null, MockBehavior.Loose).Object;

            var startPinStub = CreatePinMock(null, MockBehavior.Loose);

            Pin startPin = startPinStub.Object;

            target.Connect(startPin, endPin);

            Assert.AreEqual(startPin.Lifeline, target.Lifeline);
        }

        [Test]
        public void OrientationTestLevel0()
        {
            var target = new Activity(0);
            Orientation actual = target.Orientation;
            Assert.AreEqual(Orientation.None, actual);
        }

        [Test]
        public void OrientationTestLevel1_StartNull()
        {
            var target = new Activity(1);
            Orientation actual = target.Orientation;
            Assert.AreEqual(Orientation.None, actual);
        }

        [Test]
        public void OrientationTestLevel1_StartOrientation()
        {
            var target = new Activity(1);

            var startPinMock = CreatePinMock(target);
            startPinMock.Setup(pin => pin.Orientation).Returns(Orientation.Left);

            var endPinStub = CreatePinMock(null, MockBehavior.Loose);

            Pin startPin = startPinMock.Object;
            Pin endPin = endPinStub.Object;
            target.Connect(startPin, endPin);

            Orientation actual = target.Orientation;
            Assert.AreEqual(Orientation.Left, actual);

            startPinMock.VerifyAll();
        }

        [Test]
        public void ReconnectEndTest()
        {
            var target = new Activity(0);

            var startPin = CreatePinMock(target, MockBehavior.Loose).Object;
            var initialEndPin = CreatePinMock(target, MockBehavior.Loose).Object;

            target.Connect(startPin, initialEndPin);

            var endPinMock = CreatePinMock(target);

            Pin endPin = endPinMock.Object;

            target.ReconnectEnd(endPin);

            Assert.AreEqual(endPin, target.End);

            endPinMock.VerifyAll();
        }
    }
}