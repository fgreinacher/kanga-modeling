using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.ModelComponents
{
    [TestFixture]
    public class RootFragmentTest
    {
     

        [Test]
        public void FragmentTypeTest()
        {
            var target = new RootFragment(); 
            OperatorType actual;
            actual = target.OperatorType;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void RootFragmentConstructorTest()
        {
            var target = new RootFragment();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        public void SetTitleTest()
        {
            var target = new RootFragment(); 
            string title = string.Empty; 
            target.SetTitle(title);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void TitleTest()
        {
            var target = new RootFragment(); 
            string actual;
            actual = target.Title;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}