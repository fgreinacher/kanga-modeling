using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using KangaModeling.Compiler.ClassDiagrams;

namespace KangaModeling.Compiler.Test.ClassDiagrams
{

    /// <summary>
    /// Tests for tokenizing a string with the class diagram scanner.
    /// </summary>
    [TestFixture]
    public class T00ScannerTests
    {

        [Test]
        public void t00_Check_Constructing()
        {
            new ClassDiagramScanner();
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void t01_Throws_On_Null_Argument()
        {
            var scanner = new ClassDiagramScanner();
            scanner.Parse(null);
        }

        [Test]
        public void t02_Check_Simple_Classes()
        {
            const string source = "[ClassName]";
            var expectedTokens = new[] { new ClassDiagramToken(0, 1, TokenType.BracketOpen), new ClassDiagramToken(0, 10, TokenType.Identifier, "ClassName"), new ClassDiagramToken(0, 11, TokenType.BracketClose)};
            checkTokens(source, expectedTokens);
        }

        [Test]
        public void t02_Check_Simple_Classes_Multiline()
        {
            var source = "[" + Environment.NewLine + "ClassName" + Environment.NewLine + "]";
            var expectedTokens = new[] { 
                new ClassDiagramToken(0, 1, TokenType.BracketOpen), 
                new ClassDiagramToken(1, 9, TokenType.Identifier, "ClassName"), 
                new ClassDiagramToken(2, 1, TokenType.BracketClose), 
            };
            checkTokens(source, expectedTokens);
        }

        [Test]
        public void t02_Check_Simple_Classes_Whitespace()
        {
            const string source = "  [  ClassName  ]  ";
            var expectedTokens = new[] { new ClassDiagramToken(0, 3, TokenType.BracketOpen), new ClassDiagramToken(0, 14, TokenType.Identifier, "ClassName"), new ClassDiagramToken(0, 17, TokenType.BracketClose), };
            checkTokens(source, expectedTokens);
        }

        [Test]
        public void t03_Check_Invalid_Identifier()
        {
            const string source = " 0IdentifiersMustStartWithALetter  ";
            var expectedTokens = new ClassDiagramToken[] { /* TODO currently invalid tokens are just ignored. */ };
            checkTokens(source, expectedTokens);
        }

        [TestCase("|", TokenType.Pipe, TestName = "pipe")]
        [TestCase(":", TokenType.Colon, TestName = "colon")]
        [TestCase(",", TokenType.Comma, TestName = "comma")]
        [TestCase("-", TokenType.Dash, TestName="dash")]
        [TestCase("<", TokenType.AngleOpen, TestName = "angle open")]
        [TestCase(">", TokenType.AngleClose, TestName = "angle close")]
        [TestCase("+", TokenType.Plus, TestName = "plus")]
        [TestCase("#", TokenType.Hash, TestName = "hash")]
        [TestCase("[", TokenType.BracketOpen, TestName = "bracket open")]
        [TestCase("]", TokenType.BracketClose, TestName = "bracket close")]
        [TestCase("(", TokenType.ParenthesisOpen, TestName = "parenthesis open")]
        [TestCase(")", TokenType.ParenthesisClose, TestName = "parenthesis close")]
        [TestCase("~", TokenType.Tilde, TestName = "tilde")]
        [TestCase("*", TokenType.Star, TestName = "star")]
        [TestCase("..", TokenType.DotDot, TestName = "dot dot")]
        [TestCase("0", TokenType.Number, TestName = "Number 0")]
        [TestCase("1", TokenType.Number, TestName = "Number 1")]
        [TestCase("12234", TokenType.Number, TestName = "Number 12234")]
        public void t04_Check_Token(String assoc, TokenType expectedTType)
        {
            var expectedTokens = new[] { new ClassDiagramToken(0, assoc.Length, expectedTType, assoc)};
            checkTokens(assoc, expectedTokens);
        }

        private void checkTokens(string source, IEnumerable<ClassDiagramToken> expectedTokens)
        {
            var scanner = new ClassDiagramScanner();
            var tokens = scanner.Parse(source).ToList();
            CollectionAssert.AreEqual(expectedTokens, tokens, "token unexpected");
        }

    }

}
