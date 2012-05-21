using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using KangaModeling.Compiler.ClassDiagrams;
using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModeling.Compiler.Test.ClassDiagrams
{

    /// <summary>
    /// Tests for tokenizing a string with the class diagram scanner.
    /// </summary>
    [TestFixture]
    public class t00_ScannerTests
    {

        [Test]
        public void t00_Check_Constructing()
        {
            var scanner = new CDScanner();
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void t01_Throws_On_Null_Argument()
        {
            var scanner = new CDScanner();
            scanner.parse(null);
        }

        [Test]
        public void t02_Check_Simple_Classes()
        {
            var source = "[ClassName]";
            var expectedTokens = new CDToken[] { new CDToken(0, 1, CDTokenType.Bracket_Open), new CDToken(0, 10, CDTokenType.Identifier, "ClassName"), new CDToken(0, 11, CDTokenType.Bracket_Close), };
            checkTokens(source, expectedTokens);
        }

        [Test]
        public void t02_Check_Simple_Classes_Multiline()
        {
            var source = "[" + Environment.NewLine + "ClassName" + Environment.NewLine + "]";
            var expectedTokens = new CDToken[] { 
                new CDToken(0, 1, CDTokenType.Bracket_Open), 
                new CDToken(1, 9, CDTokenType.Identifier, "ClassName"), 
                new CDToken(2, 1, CDTokenType.Bracket_Close), 
            };
            checkTokens(source, expectedTokens);
        }

        [Test]
        public void t02_Check_Simple_Classes_Whitespace()
        {
            var source = "  [  ClassName  ]  ";
            var expectedTokens = new CDToken[] { new CDToken(0, 3, CDTokenType.Bracket_Open), new CDToken(0, 14, CDTokenType.Identifier, "ClassName"), new CDToken(0, 17, CDTokenType.Bracket_Close), };
            checkTokens(source, expectedTokens);
        }

        [Test]
        public void t03_Check_Invalid_Identifier()
        {
            var source = " 0IdentifiersMustStartWithALetter  ";
            var expectedTokens = new CDToken[] { /* TODO currently invalid tokens are just ignored. */ };
            checkTokens(source, expectedTokens);
        }

        [TestCase(",", CDTokenType.Comma, TestName = "comma")]
        [TestCase("-", CDTokenType.Dash, TestName="dash")]
        [TestCase("<", CDTokenType.Angle_Open, TestName = "angle open")]
        [TestCase(">", CDTokenType.Angle_Close, TestName = "angle close")]
        [TestCase("+", CDTokenType.Plus, TestName = "plus")]
        [TestCase("#", CDTokenType.Hash, TestName = "hash")]
        [TestCase("[", CDTokenType.Bracket_Open, TestName = "bracket open")]
        [TestCase("]", CDTokenType.Bracket_Close, TestName = "bracket close")]
        [TestCase("*", CDTokenType.Star, TestName = "star")]
        [TestCase("..", CDTokenType.DotDot, TestName = "dot dot")]
        [TestCase("0", CDTokenType.Number, TestName = "Number")]
        [TestCase("1", CDTokenType.Number, TestName = "Number")]
        [TestCase("12234", CDTokenType.Number, TestName = "Number")]
        [Test]
        public void t04_Check_Token(String assoc, CDTokenType expectedTType)
        {
            var expectedTokens = new CDToken[] { new CDToken(0, assoc.Length, expectedTType, assoc), };
            checkTokens(assoc, expectedTokens);
        }

        private void checkTokens(string source, CDToken[] expectedTokens)
        {
            var scanner = new CDScanner();
            var tokens = new List<CDToken>(scanner.parse(source));
            CollectionAssert.AreEqual(expectedTokens, tokens, "token unexpected");
        }

    }

}
