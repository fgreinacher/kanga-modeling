using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class RowsCollectionTest
    {
        [Test]
        public void ExtendTest()
        {
            var target = new RowsCollection(); 
            Lifeline lifeline = null; 
            target.Extend(lifeline);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void RowsCollectionConstructorTest()
        {
            var target = new RowsCollection();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}