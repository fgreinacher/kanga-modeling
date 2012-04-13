using System;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using Moq;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
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
        public void LevelTest(int level)
        {
            Pin target = CreatePin();
            Assert.AreEqual(0, target.Level);
            Activity expected = new Activity(level);
            target.SetActivity(expected);
            Assert.AreEqual(level, target.Level);
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

        [Test]
        public abstract void OrientationTest();

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

        [Test]
        public void RowIndexTest()
        {
            Pin target = CreatePin(); 
            int actual;
            actual = target.RowIndex;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        
        public void RowTest()
        {
            //PrivateObject param0 = null; 
            //var target = new Pin_Accessor(param0); 
            //Row_Accessor expected = null; 
            //Row_Accessor actual;
            //target.Row = expected;
            //actual = target.Row;
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void SetActivityTest()
        {
            Pin target = CreatePin(); 
            Activity activity = null; 
            target.SetActivity(activity);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void SetLevelTest()
        {
            Pin target = CreatePin(); 
            int level = 0;
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void SetSignalTest()
        {
            Pin target = CreatePin(); 
            Signal signal = null; 
            target.SetSignal(signal);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void SignalTest()
        {
            Pin target = CreatePin(); 
            ISignal actual;
            actual = target.Signal;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}