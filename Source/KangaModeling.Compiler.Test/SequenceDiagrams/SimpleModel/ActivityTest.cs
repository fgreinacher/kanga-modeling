using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using Moq;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class ActivityTest
    {
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

            var startPinMock = new Mock<Pin>(MockBehavior.Strict);
            startPinMock.Setup(pin => pin.SetActivity(target));

            var endPinMock = new Mock<Pin>(MockBehavior.Strict);
            endPinMock.Setup(pin => pin.SetActivity(target));

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

            var startPinStub = new Mock<Pin>(MockBehavior.Loose);

            var endPinStub = new Mock<Pin>(MockBehavior.Loose);
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

            var startPinStub = new Mock<Pin>(MockBehavior.Loose);
            startPinStub.Setup(pin => pin.RowIndex).Returns(rowIndex);

            var endPinStub = new Mock<Pin>(MockBehavior.Loose);

            target.Connect(startPinStub.Object, endPinStub.Object);

            int actual = target.StartRowIndex;
            Assert.AreEqual(rowIndex, actual);
        }

        [Test]
        public void LifelineTest()
        {
            var target = new Activity(0);

            ILifeline lifeline = new Mock<ILifeline>(MockBehavior.Loose).Object;
            Pin endPin = new Mock<Pin>(MockBehavior.Loose).Object;

            var startPinStub = new Mock<Pin>(MockBehavior.Loose);
            startPinStub.Setup(pin => pin.Lifeline).Returns(lifeline);

            Pin startPin = startPinStub.Object;
            
            target.Connect(startPin, endPin);

            Assert.AreEqual(lifeline, target.Lifeline);
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

            var startPinMock = new Mock<Pin>(MockBehavior.Strict);
            startPinMock.Setup(pin => pin.SetActivity(target));
            startPinMock.Setup(pin => pin.Orientation).Returns(Orientation.Left);

            var endPinStub = new Mock<Pin>(MockBehavior.Loose);

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

            var endPinMock = new Mock<Pin>(MockBehavior.Strict);
            endPinMock.Setup(pin => pin.SetActivity(target));

            Pin endPin = endPinMock.Object;
            target.ReconnectEnd(endPin);

            Assert.AreEqual(endPin, target.End);

            endPinMock.VerifyAll();
        }
    }
}