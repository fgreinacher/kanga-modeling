using System;
using KangaModeling.Compiler.SequenceDiagrams.Model;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.ModelComponents
{
    [TestFixture]
    public class LifelineCollectionTest
    {
        [Test]
        public void LifelineCollectionConstructorTest()
        {
            var target = new LifelineCollection(StringComparer.InvariantCultureIgnoreCase);
            Assert.AreEqual(0, target.Count);
        }

        [Test]
        public void TryGetValueTest_Positive()
        {
            var target = new LifelineCollection(StringComparer.InvariantCultureIgnoreCase);
            const string key = "key";
            Lifeline lifeline = new Lifeline(null, key, null, 0);
            Lifeline lifelineExpected = lifeline;
            target.Add(lifeline);
            bool actual = target.TryGetValue(key, out lifeline);
            Assert.AreEqual(lifelineExpected, lifeline);
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void TryGetValueTest_Negative()
        {
            var target = new LifelineCollection(StringComparer.InvariantCultureIgnoreCase);
            const string key = "key";
            Lifeline lifeline = new Lifeline(null, key+"bullshit", null, 0);
            target.Add(lifeline);
            Lifeline lifelineExpected = null;
            bool actual = target.TryGetValue(key, out lifeline);
            Assert.AreEqual(lifelineExpected, lifeline);
            Assert.AreEqual(false, actual);
        }


        [Test]
        public void TryGetValueTest_Multiple_Items()
        {
            var target = new LifelineCollection(StringComparer.InvariantCultureIgnoreCase);
            const string key1 = "key1";
            Lifeline lifeline1 = new Lifeline(null, key1, null, 0);
            Lifeline lifelineExpected1 = lifeline1;
            target.Add(lifeline1);
        
            const string key2 = "key2";
            Lifeline lifeline2 = new Lifeline(null, key2 + "bullshit", null, 0);
            Lifeline lifelineExpected2 = null;
            target.Add(lifeline2);

            bool actual = target.TryGetValue(key1, out lifeline1);
            Assert.AreEqual(lifelineExpected1, lifeline1);
            Assert.AreEqual(true, actual);

            actual = target.TryGetValue(key2, out lifeline2);
            Assert.AreEqual(lifelineExpected2, lifeline2);
            Assert.AreEqual(false, actual);
        }
    }
}