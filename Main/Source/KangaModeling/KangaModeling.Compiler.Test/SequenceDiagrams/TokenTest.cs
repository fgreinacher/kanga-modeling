using KangaModeling.Compiler.SequenceDiagrams.Reading;
using NUnit.Framework;
using System;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class TokenTest
    {
        [TestCase(0, null, ExpectedException = typeof(ArgumentNullException))]
        [TestCase(0, "some text", ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(0, "")]
        public void TokenConstructorTest(int expectedEnd, string expectedValue)
        {
            Token target = new Token(expectedEnd, expectedValue);
            Assert.AreEqual(expectedEnd, target.End);
        }

        [TestCase(0, "")]
        [TestCase(3, "abc")]
        [TestCase(5, "abcd")]
        public void EndTest(int end, string value)
        {
            Token target = new Token(end, value);
            Assert.AreEqual(end, target.End);
        }

        [TestCase(0, "", 0)]
        [TestCase(3, "abc", 3)]
        [TestCase(5, "abcd", 4)]
        public void LengthTest(int end, string value, int expectedLength)
        {
            Token target = new Token(end, value);
            Assert.AreEqual(expectedLength, target.Length);
        }

        [TestCase(0, "", 0)]
        [TestCase(3, "abc", 0)]
        [TestCase(5, "abcd", 1)]
        public void StartTest(int end, string value, int expectedStart)
        {
            Token target = new Token(end, value);
            Assert.AreEqual(expectedStart, target.Start);
        }

        [TestCase(0, "")]
        [TestCase(3, "abc")]
        [TestCase(5, "abcd")]
        public void ValueTest(int end, string value)
        {
            Token target = new Token(end, value);
            Assert.AreEqual(value, target.Value);
        }
    }
}
