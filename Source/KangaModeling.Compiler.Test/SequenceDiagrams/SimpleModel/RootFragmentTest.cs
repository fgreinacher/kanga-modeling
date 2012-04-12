using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class RootFragmentTest
    {
     
        [Test]
        public void BottomTest()
        {
            Matrix matrix = null; 
            var target = new RootFragment(matrix); 
            int actual;
            actual = target.Bottom;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void FragmentTypeTest()
        {
            Matrix matrix = null; 
            var target = new RootFragment(matrix); 
            FragmentType actual;
            actual = target.FragmentType;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void LeftTest()
        {
            Matrix matrix = null; 
            var target = new RootFragment(matrix); 
            ILifeline actual;
            actual = target.Left;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void RightTest()
        {
            Matrix matrix = null; 
            var target = new RootFragment(matrix); 
            ILifeline actual;
            actual = target.Right;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void RootFragmentConstructorTest()
        {
            Matrix matrix = null; 
            var target = new RootFragment(matrix);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        public void SetTitleTest()
        {
            Matrix matrix = null; 
            var target = new RootFragment(matrix); 
            string title = string.Empty; 
            target.SetTitle(title);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void TitleTest()
        {
            Matrix matrix = null; 
            var target = new RootFragment(matrix); 
            string actual;
            actual = target.Title;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void TopTest()
        {
            Matrix matrix = null; 
            var target = new RootFragment(matrix); 
            int actual;
            actual = target.Top;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}