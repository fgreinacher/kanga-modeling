using System;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.Model;
using Moq;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.ModelComponents
{
    [TestFixture]
    public abstract class PinTest
    {
        internal Pin CreatePin()
        {
            return CreatePin(null, null);
        }

        internal abstract Pin CreatePin(Row row, Lifeline lifeline);

        [Test]
        public void ActivityTest()
        {
            Pin target = CreatePin();
            Assert.IsNull(target.Activity);
            Activity expected = new Activity(0);
            target.SetActivity(expected);
            IActivity actual = target.Activity;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetActivityTest()
        {
            Pin target = CreatePin();
            Assert.IsNull(target.GetActivity());
            Activity expected = new Activity(0);
            target.SetActivity(expected);
            Activity actual = target.GetActivity();
            Assert.AreEqual(expected, actual);
        }
        
        [TestCase(0)]
        [TestCase(10)]
        public void LifelineIndexTest(int index)
        {
            var lifelineMock = new Mock<Lifeline>(MockBehavior.Strict, null, null, null, 0);
            lifelineMock.Setup(lifeline => lifeline.Index).Returns(index);

            Pin target = CreatePin(null, lifelineMock.Object); 
            int actual = target.LifelineIndex;
            Assert.AreEqual(index, actual);

            lifelineMock.VerifyAll();
        }

        [Test]
        public void LifelineTest()
        {
            var lifelineStub = new Mock<Lifeline>(MockBehavior.Loose, null, null, null, 0);

            Pin target = CreatePin(null, lifelineStub.Object);
            ILifeline actual = target.Lifeline;
            Assert.AreEqual(lifelineStub.Object, actual);
        }

        [TestCase(PinType.None)]
        [TestCase(PinType.In)]
        [TestCase(PinType.Out)]
        public void PinTypeTest(PinType expected)
        {
            Pin target = CreatePin();
            Assert.AreEqual(PinType.None, target.PinType);
            target.PinType = expected;
            PinType actual = target.PinType;
            Assert.AreEqual(expected, actual);
        }

        [TestCase(0)]
        [TestCase(10)]
        public virtual void RowIndexTest(int index)
        {
            var rowMock = new Mock<Row>(MockBehavior.Strict, 0);
            rowMock.Setup(row => row.Index).Returns(index);

            Pin target = CreatePin(rowMock.Object, null);
            int actual = target.RowIndex;
            Assert.AreEqual(index, actual);

            rowMock.VerifyAll();
        }

        [Test]
        public virtual void RowTest()
        {
            var rowStub = new Mock<Row>(MockBehavior.Loose, 0);

            Pin target = CreatePin(rowStub.Object, null);
            Row actual = target.Row;
            Assert.AreEqual(rowStub.Object, actual);
        }

        [Test]
        public void SetSignalTest()
        {
            var signalStub = new Mock<Signal>(MockBehavior.Loose, null);

            Pin target = CreatePin();
            Assert.IsNull(target.Signal);

            Signal signal = signalStub.Object; 
            target.SetSignal(signal);
            Assert.AreEqual(signal, target.Signal);
        }
    }
}