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
            var expectedTokens = new Token[]{ new Token(0, 1, "["), new Token(0, 10, "ClassName"), new Token(0, 11, "]"), };
            checkTokens(source, expectedTokens);
        }

        [Test]
        public void t02_Check_Simple_Classes_Whitespace()
        {
            var source = "  [  ClassName  ]  ";
            var expectedTokens = new Token[] { new Token(0, 3, "["), new Token(0, 14, "ClassName"), new Token(0, 17, "]"), };
            checkTokens(source, expectedTokens);
        }

        [Test]
        public void t03_Check_Invalid_Identifier()
        {
            var source = " 0IdentifiersMustStartWithALetter  ";
            var expectedTokens = new Token[] { /* TODO currently invalid tokens are just ignored. */ };
            checkTokens(source, expectedTokens);
        }

        private void checkTokens(string source, Token[] expectedTokens)
        {
            var scanner = new CDScanner();
            var tokens = new List<Token>(scanner.parse(source));
            CollectionAssert.AreEqual(expectedTokens, tokens, "token unexpected");
        }

    }

}
