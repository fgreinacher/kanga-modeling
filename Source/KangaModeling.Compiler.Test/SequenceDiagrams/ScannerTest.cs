using KangaModeling.Compiler.SequenceDiagrams;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class Scanners
    {
        [TestCase("")]
        [TestCase("a")]
        [TestCase("a\r\nb")]
        [TestCase("a\r\nb\r\nc")]
        [TestCase("a\r\n\r\nc")]
        public void Scanner_as_Enumerator(string input)
        {
            List<string> actual = new List<string>();
            string[] expected = input.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            using (Scanner scanner = new Scanner(input))
            while (scanner.MoveNext())
            {
                actual.Add(scanner.Current);
            }
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void Ensure_inner_enumerator_gets_disposed()
        {
            var enumerableMock = new Mock<IEnumerable<string>>(MockBehavior.Strict);
            var enumeratorMock = new Mock<IEnumerator<string>>(MockBehavior.Strict);
            enumerableMock.Setup(enumerable => enumerable.GetEnumerator()).Returns(enumeratorMock.Object);
            enumeratorMock.Setup(enumerator => enumerator.Dispose());

            Scanner target = new Scanner(enumerableMock.Object);
            target.Dispose();
            
            enumerableMock.Verify();
            enumeratorMock.Verify();
        }

        [TestCase("a", "a")]
        [TestCase(" a", "a")]
        [TestCase(" a ", "a")]
        [TestCase("       Hello ", "Hello")]
        [TestCase("alt two > one", "alt")]
        [TestCase("alt two < one", "alt")]
        [TestCase("alt two - one", "alt")]
        [TestCase("alt two <- one", "<-")]
        [TestCase("alt two <-- one", "<--")]
        [TestCase("alt two -> one", "->")]
        [TestCase("alt two --> one", "-->")]
        public void GetKeyWordTest(string input, string expected)
        {
            Scanner target = new Scanner(input);
            target.MoveNext();
            string actual = target.GetKeyWord();
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(0, target.Column);
        }

        [TestCase("|a", "|a", 0)]
        [TestCase(" a", "", 2)]
        [TestCase("abcd|ABCD ", "|ABCD ", 4)]
        public void ReadToEndTest(string input, string expectedValue, int expectedStart)
        {
            Scanner target = new Scanner(input);
            target.MoveNext();
            target.SkipWhile(ch=>ch!='|');
            Token actual = target.ReadToEnd();
            Assert.AreEqual(expectedValue, actual.Value);
            Assert.AreEqual(expectedStart, actual.Start);
        }

        [TestCase("a", "a", 1)]
        [TestCase(" a", "a", 2)]
        [TestCase(" a ", "a", 2)]
        [TestCase("       Hello ", "Hello", 12)]
        [TestCase("    \"Hello World!\"   ", "Hello World!", 18)]
        [TestCase("    \"\"   ", "", 6)]
        public void ReadWordTest(string input, string expectedValue, int expectedColumn)
        {
            Scanner target = new Scanner(input);
            target.MoveNext();
            Token actual = target.ReadWord();
            Assert.AreEqual(expectedValue, actual.Value);
            Assert.AreEqual(expectedColumn, target.Column);
        }

        [TestCase("a", "", 1)]

        [TestCase("->", "->", 2)]
        [TestCase("-->", "-->", 3)]
        [TestCase("<-", "<-", 2)]
        [TestCase("<--", "<--", 3)]

        [TestCase("->   ", "->", 2)]
        [TestCase("-->  ", "-->", 3)]
        [TestCase("<-  ", "<-", 2)]
        [TestCase("<--  ", "<--", 3)]

        [TestCase("->XXX","->",2)]
        [TestCase("-->XX","-->",3)]
        [TestCase("<-XX","<-",2)]
        [TestCase("<--XX","<--",3)]

        [TestCase("YYY->XXX", "->", 5)]
        [TestCase("YYY-->XX", "-->", 6)]
        [TestCase("YYY<-XX", "<-", 5)]
        [TestCase("YYY<--XX", "<--", 6)]
        public void ReadSignalTest(string input, string expectedValue, int expectedColumn)
        {
            Scanner target = new Scanner(input);
            target.MoveNext();
            Token actual = target.ReadSignal();
            Assert.AreEqual(expectedValue, actual.Value);
            Assert.AreEqual(expectedColumn, target.Column);
        }


        [TestCase("a", "a", "")]
        [TestCase("abcd AS", "AS", "abcd")]
        [TestCase("abcd AS rest", "AS", "abcd")]
        [TestCase("abcd     AS     rest", "AS", "abcd")]
        [TestCase("abcd Hello ", "AS", "abcd Hello ")]
        public void ReadTo(string input, string keyWord, string expectedValue)
        {
            Scanner target = new Scanner(input);
            target.MoveNext();
            Token actual = target.ReadTo(keyWord);
            Assert.AreEqual(expectedValue, actual.Value);
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("a\r\nb")]
        [TestCase("a\r\nb\r\nc")]
        [TestCase("a\r\n\r\nc")]
        public void ResetTest(string input)
        {
            Scanner target = new Scanner(input);
            if (target.MoveNext())
            {
                target.ReadToEnd();
            }
            target.Reset();

            Assert.AreEqual(0, target.Column);
            Assert.AreEqual(0, target.Line);
        }

        [TestCase("")]
        public void EolTest_Empty(string input)
        {
            using (Scanner target = new Scanner(input))
            {
                target.MoveNext();
                Assert.IsTrue(target.Eol);
                target.ReadToEnd();
                Assert.IsTrue(target.Eol);
            }
        }

        [TestCase("a")]
        [TestCase("a\r\nb")]
        [TestCase("a\r\nb\r\nc")]
        public void EolTest(string input)
        {
            using(Scanner target = new Scanner(input))
            while (target.MoveNext())
            {
                Assert.IsFalse(target.Eol);
                target.ReadToEnd();
                Assert.IsTrue(target.Eol);
            }
        }
    }
}