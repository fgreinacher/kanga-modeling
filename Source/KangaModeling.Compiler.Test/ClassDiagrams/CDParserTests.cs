using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using KangaModeling.Compiler.ClassDiagrams;
using KangaModeling.Compiler.SequenceDiagrams;
using Moq;


namespace KangaModeling.Compiler.Test.ClassDiagrams
{

    /// <summary>
    /// Tests for tokenizing a string with the class diagram scanner.
    /// </summary>
    [TestFixture]
    public class t01_ParserTests
    {

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void t00_Ctor_Throws_On_Null_Argument()
        {
            var parser = new CDParser(null);
        }

        [Test(Description="[ClassName]")]
        public void t01_Parse_Simple_Class()
        {
            var tokens = new TokenStream(){ new Token(0, 1, "["), new Token(0, 10, "ClassName"), new Token(0, 11, "]"), };
            var parser = new CDParser(tokens);
            var clazz = parser.parseClass();
            Assert.AreEqual("ClassName", clazz.Name);
        }

        [Test(Description = "ClassName]")]
        public void t02_Parse_Simple_Class_Missing_Start_Bracket()
        {
            var tokens = new TokenStream() { new Token(0, 9, "ClassName"), new Token(0, 10, "]"), };
            var parser = new CDParser(tokens);
            var clazz = parser.parseClass(); // TODO error handling?
            Assert.IsNull(clazz, "invalid");
        }

        [Test(Description = "[ClassName")]
        public void t03_Parse_Simple_Class_Missing_End_Bracket()
        {
            var tokens = new TokenStream() { new Token(0, 1, "["), new Token(0, 10, "ClassName"), };
            var parser = new CDParser(tokens);
            var clazz = parser.parseClass(); // TODO error handling?
            Assert.IsNull(clazz, "invalid");
        }

        [Test(Description = "")]
        public void t04_Parse_Simple_Class_No_Tokens()
        {
            var tokens = new TokenStream() { };
            var parser = new CDParser(tokens);
            var clazz = parser.parseClass(); // TODO error handling?
            Assert.IsNull(clazz, "invalid");
        }

    }

}
