using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class PinTest
    {
        internal virtual Pin CreatePin()
        {
            // TODO: Instantiate an appropriate concrete class.
            Pin target = null;
            return target;
        }


        [Test]
        public void ActivityTest()
        {
            Pin target = CreatePin(); 
            Activity actual;
            actual = target.Activity;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        
        public void ActivityTest1()
        {
            IPin target = CreatePin(); 
            IActivity actual;
            actual = target.Activity;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void GetActivityTest()
        {
            Pin target = CreatePin(); 
            Activity expected = null; 
            Activity actual;
            actual = target.GetActivity();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void LevelTest()
        {
            Pin target = CreatePin(); 
            int actual;
            actual = target.Level;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void LifelineIndexTest()
        {
            Pin target = CreatePin(); 
            int actual;
            actual = target.LifelineIndex;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void LifelineTest()
        {
            Pin target = CreatePin(); 
            ILifeline actual;
            actual = target.Lifeline;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void OrientationTest()
        {
            Pin target = CreatePin(); 
            Orientation actual;
            actual = target.Orientation;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void PinTypeTest()
        {
            Pin target = CreatePin(); 
            var expected = new PinType(); 
            PinType actual;
            target.PinType = expected;
            actual = target.PinType;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
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
            target.SetLevel(level);
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