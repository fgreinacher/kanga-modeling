using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class SectionFragmentTest
    {
        [Test]
        public void FragmentTypeTest()
        {
            Fragment parent = null; 
            string title = string.Empty; 
            var target = new Operand(parent, title); 
            FragmentType actual;
            actual = target.FragmentType;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void SectionFragmentConstructorTest()
        {
            Fragment parent = null; 
            string title = string.Empty; 
            var target = new Operand(parent, title);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        public void SetBottomTest()
        {
            Fragment parent = null; 
            string title = string.Empty; 
            var target = new Operand(parent, title); 
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void TitleTest()
        {
            Fragment parent = null; 
            string title = string.Empty; 
            var target = new Operand(parent, title); 
            string actual;
            actual = target.Title;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}