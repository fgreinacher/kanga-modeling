using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using Moq;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.ModelComponents
{
    [TestFixture]
    public abstract class SignalTest
    {
        internal abstract Signal CreateSignal();

        [Test]
        public void ConnectTest()
        {
            Signal target = CreateSignal();
            var startPinMock = new Mock<Pin>(MockBehavior.Strict);
            startPinMock.Setup(pin => pin.SetSignal(target));
            startPinMock.SetupProperty(pin => pin.PinType, PinType.None);

            var endPinMock = new Mock<Pin>(MockBehavior.Strict);
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

            var startPinStub = new Mock<Pin>(MockBehavior.Loose);
            startPinStub.Setup(pin => pin.RowIndex).Returns(rowIndex);

            var endPinStub = new Mock<Pin>(MockBehavior.Loose);

            target.Connect(startPinStub.Object, endPinStub.Object);

            int actual = target.RowIndex;
            Assert.AreEqual(rowIndex, actual);
        }
    }
}