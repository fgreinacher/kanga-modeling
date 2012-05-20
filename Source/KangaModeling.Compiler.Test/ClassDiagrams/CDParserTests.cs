using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using KangaModeling.Compiler.ClassDiagrams;
using KangaModeling.Compiler.SequenceDiagrams;
using Moq;
using KangaModeling.Compiler.ClassDiagrams.Model;


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

        [Test]
        public void t06_Parse_ClassDiagram_Containing_One_Class()
        {
            var tokens = new TokenStream() { 
                createToken("["), createToken("a"), createToken("]"),
            };
            var parser = new CDParser(tokens);
            var cd = parser.parseClassDiagram();
            Assert.IsNotNull(cd, "parsing failed");

            var classes = cd.Classes.ToList();
            Assert.AreEqual(1, classes.Count, "wrong class count");
            Assert.AreEqual("a", classes[0].Name, "unexpected class name");
        }

        [Test]
        public void t06_Parse_ClassDiagram_Containing_Two_Classes()
        {
            var tokens = new TokenStream() { 
                createToken("["), createToken("a"), createToken("]"),
                createToken(","),
                createToken("["), createToken("b"), createToken("]"),
            };
            var parser = new CDParser(tokens);
            var cd = parser.parseClassDiagram();
            Assert.IsNotNull(cd, "parsing failed");

            var classes = cd.Classes.ToList();
            Assert.AreEqual(2, classes.Count, "wrong class count");
            Assert.AreEqual("a", classes[0].Name, "unexpected class name");
            Assert.AreEqual("b", classes[1].Name, "unexpected class name");
        }

        [TestCase("->", AssociationKind.Directed, TestName="Directed")]
        [TestCase("-", AssociationKind.Undirected, TestName = "Undirected")]
        [TestCase("+->", AssociationKind.Aggregation, TestName = "Aggregation")]
        [TestCase("++->", AssociationKind.Composition, TestName = "Composition")]
        [TestCase("<>->", AssociationKind.Aggregation, TestName = "Aggregation")]
        [Test]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(String assoc, AssociationKind expectedKind)
        {
            var tokens = new TokenStream() { 
                createToken("["), createToken("a"), createToken("]"),
                createToken(assoc),
                createToken("["), createToken("b"), createToken("]"),
            };
            var parser = new CDParser(tokens);
            var cd = parser.parseClassDiagram();
            Assert.IsNotNull(cd, "parsing failed");

            var classes = cd.Classes.ToList();
            Assert.AreEqual(2, classes.Count, "wrong class count");
            Assert.AreEqual("a", classes[0].Name, "unexpected class name");
            Assert.AreEqual("b", classes[1].Name, "unexpected class name");

            var assocs = cd.Associations.ToList();
            Assert.AreEqual(1, assocs.Count, "wrong association count");
            Assert.AreEqual("a", assocs[0].Source.Name, "Source name wrong");
            Assert.AreEqual("b", assocs[0].Target.Name, "Target name wrong");
            Assert.AreEqual(expectedKind, assocs[0].Kind, "wrong association kind");
        }

        //[TestCase("0-", TestName = "0-")] TODO
        //[TestCase("-1", TestName = "-1")]
        [TestCase("0-0", TestName = "0-0")]
        [TestCase("0-1", TestName = "0-1")]
        [TestCase("0-0..1", TestName = "0-0..1")]
        [TestCase("0-*", TestName = "0-*")]
        [TestCase("0-0..*", TestName = "0-0..*")]
        [TestCase("0-1..*", TestName = "0-1..*")]
        [TestCase("0..*-1..*", TestName = "0..*-1..*")]
        [TestCase("*-*", TestName = "*-*")]
        [Test, Ignore("not implemented")]
        public void t08_Parse_ClassDiagram_Containing_Two_Associated_Classes_With_Multiplicities(String assoc)
        {
            var multTokens = assoc.Split(new String[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

            var tokens = new TokenStream() { 
                createToken("["), createToken("a"), createToken("]"),
                createToken(multTokens[0]), createToken("-"), createToken(multTokens[1]),
                createToken("["), createToken("b"), createToken("]"),
            };

            var parser = new CDParser(tokens);
            var cd = parser.parseClassDiagram();
            Assert.IsNotNull(cd, "parsing failed");

            var classes = cd.Classes.ToList();
            Assert.AreEqual(2, classes.Count, "wrong class count");
            Assert.AreEqual("a", classes[0].Name, "unexpected class name");
            Assert.AreEqual("b", classes[1].Name, "unexpected class name");

            var assocs = cd.Associations.ToList();
            Assert.AreEqual(1, assocs.Count, "wrong association count");
            Assert.AreEqual("a", assocs[0].Source.Name, "Source name wrong");
            Assert.AreEqual("b", assocs[0].Target.Name, "Target name wrong");
            Assert.AreEqual(AssociationKind.Undirected, assocs[0].Kind, "wrong association kind");
        }

        [Test(Description="[a] -associationName >[b]"), Ignore("not implemented")]
        public void t09_Parse_ClassDiagram_Containing_Two_Associated_Classes_With_Roles()
        {
            var tokens = new TokenStream() { 
                createToken("["), createToken("a"), createToken("]"),
                createToken("-"), createToken("associationName"), createToken(">"),
                createToken("["), createToken("b"), createToken("]"),
            };

            var parser = new CDParser(tokens);
            var cd = parser.parseClassDiagram();
            Assert.IsNotNull(cd, "parsing failed");

            var classes = cd.Classes.ToList();
            Assert.AreEqual(2, classes.Count, "wrong class count");
            Assert.AreEqual("a", classes[0].Name, "unexpected class name");
            Assert.AreEqual("b", classes[1].Name, "unexpected class name");

            var assocs = cd.Associations.ToList();
            Assert.AreEqual(1, assocs.Count, "wrong association count");
            Assert.AreEqual("a", assocs[0].Source.Name, "Source name wrong");
            Assert.AreEqual("b", assocs[0].Target.Name, "Target name wrong");
            Assert.AreEqual(AssociationKind.Directed, assocs[0].Kind, "wrong association kind");
            Assert.AreEqual("associationName", assocs[0].TargetRole, "unexpected target role");
        }

        private Token createToken(string value)
        {
            return new Token(0, value.Length, value);
        }

    }

}
