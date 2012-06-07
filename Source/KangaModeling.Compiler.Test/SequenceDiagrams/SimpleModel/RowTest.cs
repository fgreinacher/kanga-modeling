using System.Collections.Generic;
using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams.Model;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.ModelComponents
{
    [TestFixture]
    public class RowTest
    {
      
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void ExtendTest(int lifeLineCount)
        {
            var target = new Row(0);
            Lifeline lifeline = new Lifeline(null, null, null, 0);
            IEnumerable<Lifeline> lifeLines = Enumerable.Repeat(lifeline, lifeLineCount); 
            target.Extend(lifeLines);
            Assert.AreEqual(lifeLines.Count(), target.Count);
        }


        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void IndexTest(int index)
        {
            var target = new Row(index);
            Assert.AreEqual(index, target.Index);
        }

        [TestCase(1)]
        [TestCase(12)]
        public void ItemTest(int index)
        {
            var target = new Row(0); 
            Lifeline lifeline = new Lifeline(null, null, null, index);
            RegularPin expected = new RegularPin(null, null);
            for (int i = 0; i <= index; i++)
            {
                target.Add(null);
            }
            target[index] = expected;
            Pin actual = target[lifeline];
            Assert.AreEqual(expected, actual);
        }
    }
}