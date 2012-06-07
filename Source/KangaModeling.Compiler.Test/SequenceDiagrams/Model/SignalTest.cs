using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.Model;
using Moq;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.Model
{
    [TestFixture]
    public abstract class SignalTest
    {
        internal abstract Signal CreateSignal();

        [Test]
        public void ConnectTest()
        {
            Signal target = CreateSignal();
            
            var lifelineMock = new Mock<Lifeline>(null, null, null, 0);
            lifelineMock.Setup(lifeline => lifeline.Index).Returns(0);

            var startPinMock = new Mock<Pin>(MockBehavior.Strict, null, lifelineMock.Object, PinType.None);
            startPinMock.Setup(pin => pin.Orientation).Returns(Orientation.None);
            startPinMock.Setup(pin => pin.SetSignal(target));
            startPinMock.SetupProperty(pin => pin.PinType, PinType.None);

            var endPinMock = new Mock<Pin>(MockBehavior.Strict, null, lifelineMock.Object, PinType.None);
            endPinMock.Setup(pin => pin.Orientation).Returns(Orientation.None);
            endPinMock.Setup(pin => pin.SetSignal(target));
            endPinMock.SetupProperty(pin => pin.PinType, PinType.None);

            Pin startPin = startPinMock.Object;
            Pin endPin = endPinMock.Object;
            target.Connect(startPin, endPin);

            Assert.AreEqual(startPin, target.Start);
            Assert.AreEqual(endPin, target.End);

            Assert.AreEqual(startPin.PinType, PinType.Out);
            Assert.AreEqual(endPin.PinType, PinType.In);

            startPinMock.VerifyAll();
            endPinMock.VerifyAll();
        }

        [TestCase(0)]
        [TestCase(12)]
        public void RowIndexTest(int rowIndex)
        {
            Signal target = CreateSignal();

            var lifelineMock = new Mock<Lifeline>(null, null, null, 0);
            lifelineMock.Setup(lifeline => lifeline.Index).Returns(0);

            var startPinStub = new Mock<Pin>(MockBehavior.Loose, null, lifelineMock.Object, PinType.None);
            startPinStub.Setup(pin => pin.RowIndex).Returns(rowIndex);

            var endPinStub = new Mock<Pin>(MockBehavior.Loose, null, lifelineMock.Object, PinType.None);

            target.Connect(startPinStub.Object, endPinStub.Object);

            int actual = target.RowIndex;
            Assert.AreEqual(rowIndex, actual);
        }
    }
}