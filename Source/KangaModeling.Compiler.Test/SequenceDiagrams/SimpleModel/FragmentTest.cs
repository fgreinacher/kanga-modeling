using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class FragmentTest
    {
        internal virtual Fragment CreateFragment()
        {
            // TODO: Instantiate an appropriate concrete class.
            Fragment target = null;
            return target;
        }

        [Test]
        public void ChildrenTest1()
        {
            IFragment target = CreateFragment(); 
            IEnumerable<IFragment> actual;
            actual = target.Children;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void FragmentTypeTest()
        {
            Fragment target = CreateFragment(); 
            FragmentType actual;
            actual = target.FragmentType;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        
        public void ParentTest()
        {
            IFragment target = CreateFragment(); 
            IFragment actual;
            actual = target.Parent;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }


        [Test]
        public void TitleTest()
        {
            Fragment target = CreateFragment(); 
            string actual;
            actual = target.Title;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}