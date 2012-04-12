using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class CallTest
    {
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        [Test]
        public void CallConstructorTest()
        {
            string name = string.Empty; 
            var target = new Call(name);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        public void SignalTypeTest()
        {
            string name = string.Empty; 
            var target = new Call(name); 
            SignalType actual;
            actual = target.SignalType;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}