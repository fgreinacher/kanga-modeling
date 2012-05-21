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

    public static class TestHelpers
    {
        public static CDToken createToken(CDTokenType type)
        {
            // constant 10 is arbitrary and just to make the CDToken happy.
            return new CDToken(0, 10, type);
        }

        public static CDToken createToken(string value)
        {
            var tt = CDTokenType.Identifier;
            int dummy;
            if (int.TryParse(value, out dummy)) tt = CDTokenType.Number;
            if (value.Equals("*")) tt = CDTokenType.Star;
            if (value.Equals("..")) tt = CDTokenType.DotDot;
            return new CDToken(0, value.Length, tt, value);
        }
    }

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
            var tokens = new TokenStream(){ TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("ClassName"), TestHelpers.createToken(CDTokenType.Bracket_Close), };
            var parser = new CDParser(tokens);
            var clazz = parser.parseClass();
            Assert.AreEqual("ClassName", clazz.Name);
        }

        [Test(Description = "ClassName]")]
        public void t02_Parse_Simple_Class_Missing_Start_Bracket()
        {
            var tokens = new TokenStream() { TestHelpers.createToken("ClassName"), TestHelpers.createToken(CDTokenType.Bracket_Close), };
            var parser = new CDParser(tokens);
            var clazz = parser.parseClass(); // TODO error handling?
            Assert.IsNull(clazz, "invalid");
        }

        [Test(Description = "[ClassName")]
        public void t03_Parse_Simple_Class_Missing_End_Bracket()
        {
            var tokens = new TokenStream() { TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("ClassName"), };
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
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("a"), TestHelpers.createToken(CDTokenType.Bracket_Close),
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
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("a"), TestHelpers.createToken(CDTokenType.Bracket_Close),
                TestHelpers.createToken(CDTokenType.Comma),
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("b"), TestHelpers.createToken(CDTokenType.Bracket_Close),
            };
            var parser = new CDParser(tokens);
            var cd = parser.parseClassDiagram();
            Assert.IsNotNull(cd, "parsing failed");

            var classes = cd.Classes.ToList();
            Assert.AreEqual(2, classes.Count, "wrong class count");
            Assert.AreEqual("a", classes[0].Name, "unexpected class name");
            Assert.AreEqual("b", classes[1].Name, "unexpected class name");
        }

        [Test]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Directed()
        {
            var tokens = new TokenStream() { 
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("a"), TestHelpers.createToken(CDTokenType.Bracket_Close),
                TestHelpers.createToken(CDTokenType.Dash), TestHelpers.createToken(CDTokenType.Angle_Close),
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("b"), TestHelpers.createToken(CDTokenType.Bracket_Close),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Directed);
        }

        [Test]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Undirected()
        {
            var tokens = new TokenStream() { 
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("a"), TestHelpers.createToken(CDTokenType.Bracket_Close),
                TestHelpers.createToken(CDTokenType.Dash),
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("b"), TestHelpers.createToken(CDTokenType.Bracket_Close),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Undirected);
        }

        [Test]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Aggregation()
        {
            var tokens = new TokenStream() { 
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("a"), TestHelpers.createToken(CDTokenType.Bracket_Close),
                TestHelpers.createToken(CDTokenType.Plus), TestHelpers.createToken(CDTokenType.Dash), TestHelpers.createToken(CDTokenType.Angle_Close),
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("b"), TestHelpers.createToken(CDTokenType.Bracket_Close),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Aggregation);
        }

        [Test]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Aggregation2()
        {
            var tokens = new TokenStream() { 
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("a"), TestHelpers.createToken(CDTokenType.Bracket_Close),
                TestHelpers.createToken(CDTokenType.Angle_Open), TestHelpers.createToken(CDTokenType.Angle_Close), TestHelpers.createToken(CDTokenType.Dash), TestHelpers.createToken(CDTokenType.Angle_Close),
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("b"), TestHelpers.createToken(CDTokenType.Bracket_Close),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Aggregation);
        }

        [Test]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Composition()
        {
            var tokens = new TokenStream() { 
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("a"), TestHelpers.createToken(CDTokenType.Bracket_Close),
                TestHelpers.createToken(CDTokenType.Plus), TestHelpers.createToken(CDTokenType.Plus), TestHelpers.createToken(CDTokenType.Dash), TestHelpers.createToken(CDTokenType.Angle_Close),
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("b"), TestHelpers.createToken(CDTokenType.Bracket_Close),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Composition);
        }

        private void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(TokenStream tokens, AssociationKind expectedKind)
        {
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
        [TestCase("0", null, "0", null, TestName = "0-0")]
        [TestCase("0", null, "1", null, TestName = "0-1")]
        [TestCase("0", null, "0", "1", TestName = "0-0..1")]
        [TestCase("0", null, "*", null, TestName = "0-*")]
        [TestCase("0", null, "0", "*", TestName = "0-0..*")]
        [TestCase("0", null, "1", "*", TestName = "0-1..*")]
        [TestCase("0", "*", "1", "*", TestName = "0..*-1..*")]
        [TestCase("*", null, "*", null, TestName = "*-*")]
        [Test]
        public void t08_Parse_ClassDiagram_Containing_Two_Associated_Classes_With_Multiplicities_Numbers(string sourceFrom, string sourceTo, string targetFrom, string targetTo)
        {
            var tokens = createAssociationTokenStream(sourceFrom, sourceTo, targetFrom, targetTo);

            var parser = new CDParser(tokens);
            var cd = parser.parseClassDiagram();
            var assoc = t08_check(cd);

            checkMultiplicityKind(assoc.SourceMultiplicity.FromKind, sourceFrom, "source from");
            checkMultiplicityKind(assoc.SourceMultiplicity.ToKind, sourceTo, "source to");
            checkMultiplicityKind(assoc.TargetMultiplicity.FromKind, targetFrom, "target from");
            checkMultiplicityKind(assoc.TargetMultiplicity.ToKind, targetTo, "target to");

            Assert.AreEqual(sourceFrom, assoc.SourceMultiplicity.From, "source wrong value");
            Assert.AreEqual(targetFrom, assoc.TargetMultiplicity.From, "target wrong value");
        }

        private void checkMultiplicityKind(Multiplicity.Kind actualKind, string s, string debugMsg)
        {
            Multiplicity.Kind expectedKind = Multiplicity.Kind.None;
            int dummy;
            if (s == null)
                expectedKind = Multiplicity.Kind.None;
            else if (int.TryParse(s, out dummy))
                expectedKind = Multiplicity.Kind.SingleNumber;
            else if (s.Equals("*"))
                expectedKind = Multiplicity.Kind.Star;

            Assert.AreEqual(expectedKind, actualKind, debugMsg);
        }

        private TokenStream createAssociationTokenStream(string sourceFrom, string sourceTo, string targetFrom, string targetTo)
        {
            var tokens = new TokenStream();
            
            tokens.AddRange(createClassTokenStream("a"));
            tokens.AddRange(new[] { TestHelpers.createToken(sourceFrom), });
            if (sourceTo != null)
                tokens.AddRange(new[] { TestHelpers.createToken(".."), TestHelpers.createToken(sourceTo) });
            tokens.AddRange(new[] { TestHelpers.createToken(CDTokenType.Dash), });
            tokens.AddRange(new[] { TestHelpers.createToken(targetFrom), });
            if (targetTo != null)
                tokens.AddRange(new[] { TestHelpers.createToken(".."), TestHelpers.createToken(targetTo) });
            tokens.AddRange(createClassTokenStream("b"));

            return tokens;
        }

        private TokenStream createClassTokenStream(string className)
        {
            return new TokenStream()
            {
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken(className), TestHelpers.createToken(CDTokenType.Bracket_Close),
            };
        }

        private IAssociation t08_check(IClassDiagram cd)
        {
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
            return assocs[0];
        }

        [Test(Description="[a] -associationName >[b]"), Ignore("not implemented")]
        public void t09_Parse_ClassDiagram_Containing_Two_Associated_Classes_With_Roles()
        {
            var tokens = new TokenStream() { 
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("a"), TestHelpers.createToken(CDTokenType.Bracket_Close),
                TestHelpers.createToken("-"), TestHelpers.createToken("associationName"), TestHelpers.createToken(">"),
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("b"), TestHelpers.createToken(CDTokenType.Bracket_Close),
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



    }

}
