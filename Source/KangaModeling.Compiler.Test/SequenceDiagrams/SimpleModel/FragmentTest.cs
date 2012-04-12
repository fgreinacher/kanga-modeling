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
        public void BottomTest()
        {
            Fragment target = CreateFragment(); 
            int actual;
            actual = target.Bottom;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void ChildrenTest()
        {
            Fragment target = CreateFragment(); 
            IEnumerable<Fragment> actual;
            actual = target.Children;
            Assert.Inconclusive("Verify the correctness of this test method.");
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
        public void LeftTest()
        {
            Fragment target = CreateFragment(); 
            ILifeline actual;
            actual = target.Left;
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
        
        public void ParentTest1()
        {
            //PrivateObject param0 = null; 
            //var target = new Fragment_Accessor(param0); 
            //IFragment actual;
            //actual = target.Parent;
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void ParnetTest()
        {
            Fragment target = CreateFragment(); 
            Fragment actual;
            actual = target.Parnet;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void RightTest()
        {
            Fragment target = CreateFragment(); 
            ILifeline actual;
            actual = target.Right;
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

        [Test]
        public void TopTest()
        {
            Fragment target = CreateFragment(); 
            int actual;
            actual = target.Top;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}