using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class SectionFragmentTest
    {
     
        [Test]
        public void BottomTest()
        {
            Fragment parent = null; 
            string title = string.Empty; 
            int stratRowIndex = 0; 
            var target = new SectionFragment(parent, title, stratRowIndex); 
            int actual;
            actual = target.Bottom;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void FragmentTypeTest()
        {
            Fragment parent = null; 
            string title = string.Empty; 
            int stratRowIndex = 0; 
            var target = new SectionFragment(parent, title, stratRowIndex); 
            FragmentType actual;
            actual = target.FragmentType;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void LeftTest()
        {
            Fragment parent = null; 
            string title = string.Empty; 
            int stratRowIndex = 0; 
            var target = new SectionFragment(parent, title, stratRowIndex); 
            ILifeline actual;
            actual = target.Left;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void RightTest()
        {
            Fragment parent = null; 
            string title = string.Empty; 
            int stratRowIndex = 0; 
            var target = new SectionFragment(parent, title, stratRowIndex); 
            ILifeline actual;
            actual = target.Right;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void SectionFragmentConstructorTest()
        {
            Fragment parent = null; 
            string title = string.Empty; 
            int stratRowIndex = 0; 
            var target = new SectionFragment(parent, title, stratRowIndex);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        public void SetBottomTest()
        {
            Fragment parent = null; 
            string title = string.Empty; 
            int stratRowIndex = 0; 
            var target = new SectionFragment(parent, title, stratRowIndex); 
            int endRowIndex = 0; 
            target.SetBottom(endRowIndex);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void TitleTest()
        {
            Fragment parent = null; 
            string title = string.Empty; 
            int stratRowIndex = 0; 
            var target = new SectionFragment(parent, title, stratRowIndex); 
            string actual;
            actual = target.Title;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void TopTest()
        {
            Fragment parent = null; 
            string title = string.Empty; 
            int stratRowIndex = 0; 
            var target = new SectionFragment(parent, title, stratRowIndex); 
            int actual;
            actual = target.Top;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}