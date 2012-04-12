using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class ActivityTest
    {
        [Test]
        public void ActivityConstructorTest()
        {
            int level = 0; 
            var target = new Activity(level);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        public void ConnectTest()
        {
            int level = 0; 
            var target = new Activity(level); 
            Pin startPin = null; 
            Pin endPin = null; 
            target.Connect(startPin, endPin);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void EndRowIndexTest()
        {
            int level = 0; 
            var target = new Activity(level); 
            int actual;
            actual = target.EndRowIndex;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void EndTest()
        {
            int level = 0; 
            var target = new Activity(level); 
            IPin actual;
            actual = target.End;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void LevelTest()
        {
            //PrivateObject param0 = null; 
            //var target = new Activity_Accessor(param0); 
            //int expected = 0; 
            //int actual;
            //target.Level = expected;
            //actual = target.Level;
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void LifelineTest()
        {
            int level = 0; 
            var target = new Activity(level); 
            ILifeline actual;
            actual = target.Lifeline;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void OrientationTest()
        {
            int level = 0; 
            var target = new Activity(level); 
            Orientation actual;
            actual = target.Orientation;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void ReconnectEndTest()
        {
            int level = 0; 
            var target = new Activity(level); 
            Pin endPin = null; 
            target.ReconnectEnd(endPin);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void StartRowIndexTest()
        {
            int level = 0; 
            var target = new Activity(level); 
            int actual;
            actual = target.StartRowIndex;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void StartTest()
        {
            int level = 0; 
            var target = new Activity(level); 
            IPin actual;
            actual = target.Start;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}