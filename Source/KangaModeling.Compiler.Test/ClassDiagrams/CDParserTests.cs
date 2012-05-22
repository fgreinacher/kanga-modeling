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

    static class TestHelpers
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
            if (value.Equals(":")) tt = CDTokenType.Colon;
            return new CDToken(0, value.Length, tt, value);
        }

        public static TokenStream createClassTokenStream(string className, TokenStream fields = null)
        {
            var f = combineStreams(new TokenStream() { TestHelpers.createToken(CDTokenType.Pipe) }, fields);

            var ts = combineStreams(
                new TokenStream() { TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken(className) },
                fields == null ? null : f,
                new TokenStream() { TestHelpers.createToken(CDTokenType.Bracket_Close), }
            );
            return ts;
        }

        public static TokenStream createFieldTokenStream(string name, string type = null)
        {
            var stream = new TokenStream() { TestHelpers.createToken(name) };
            if (type != null)
                stream = combineStreams(stream, new TokenStream() { TestHelpers.createToken(CDTokenType.Colon), TestHelpers.createToken(type) });
            return stream;
        }

        public static TokenStream createAssociationTokenStream(string sourceFrom, string sourceTo, string targetFrom, string targetTo)
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

        public static TokenStream combineStreams(params TokenStream[] streams)
        {
            var combinedStream = new TokenStream();
            foreach (var singleStream in streams) if(singleStream != null) combinedStream.AddRange(singleStream);
            return combinedStream;
        }
    }

    /// <summary>
    /// Tests for tokenizing a string with the class diagram scanner.
    /// </summary>
    [TestFixture]
    class t01_ParserTests
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
            var clazz = new CDParser(tokens).parseClass(); // TODO error handling?
            Assert.IsNull(clazz, "invalid");

            tokens = new TokenStream() { TestHelpers.createToken("ClassName"), TestHelpers.createToken(CDTokenType.Bracket_Close), };
            var cd = new CDParser(tokens).parseClassDiagram(); // TODO error handling?
            Assert.IsNull(cd, "invalid");
        }

        [Test(Description = "[ClassName")]
        public void t03_Parse_Simple_Class_Missing_End_Bracket()
        {
            var tokens = new TokenStream() { TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("ClassName"), };
            var clazz = new CDParser(tokens).parseClass(); // TODO error handling?
            Assert.IsNull(clazz, "invalid");

            tokens = new TokenStream() { TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("ClassName"), };
            var cd = new CDParser(tokens).parseClassDiagram(); // TODO error handling?
            Assert.IsNull(cd, "invalid");
        }

        [Test(Description = "")]
        public void t04_Parse_Simple_Class_No_Tokens()
        {
            var tokens = new TokenStream() { };
            var parser = new CDParser(tokens);
            var clazz = parser.parseClass(); // TODO error handling?
            Assert.IsNull(clazz, "invalid");
        }

        [Test(Description="[a]")]
        public void t06_Parse_ClassDiagram_Containing_One_Class()
        {
            var tokens = TestHelpers.createClassTokenStream("a");

            var parser = new CDParser(tokens);
            var cd = parser.parseClassDiagram();
            Assert.IsNotNull(cd, "parsing failed");

            var classes = cd.Classes.ToList();
            Assert.AreEqual(1, classes.Count, "wrong class count");
            Assert.AreEqual("a", classes[0].Name, "unexpected class name");
        }

        [Test, Description("[a],[b]")]
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

        [Test, Description("[a]->[b]")]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Directed()
        {
            var tokens = new TokenStream() { 
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("a"), TestHelpers.createToken(CDTokenType.Bracket_Close),
                TestHelpers.createToken(CDTokenType.Dash), TestHelpers.createToken(CDTokenType.Angle_Close),
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("b"), TestHelpers.createToken(CDTokenType.Bracket_Close),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Directed);
        }

        [Test, Description("[a]-[b]")]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Undirected()
        {
            var tokens = new TokenStream() { 
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("a"), TestHelpers.createToken(CDTokenType.Bracket_Close),
                TestHelpers.createToken(CDTokenType.Dash),
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("b"), TestHelpers.createToken(CDTokenType.Bracket_Close),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Undirected);
        }

        [Test, Description("[a]+->[b]")]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Aggregation()
        {
            var tokens = new TokenStream() { 
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("a"), TestHelpers.createToken(CDTokenType.Bracket_Close),
                TestHelpers.createToken(CDTokenType.Plus), TestHelpers.createToken(CDTokenType.Dash), TestHelpers.createToken(CDTokenType.Angle_Close),
                TestHelpers.createToken(CDTokenType.Bracket_Open), TestHelpers.createToken("b"), TestHelpers.createToken(CDTokenType.Bracket_Close),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Aggregation);
        }

        [Test, Description("[a]<>->[b]")]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Aggregation_Alternative()
        {
            var tokens = TestHelpers.combineStreams(
                TestHelpers.createClassTokenStream("a"),
                new TokenStream() { 
                    TestHelpers.createToken(CDTokenType.Angle_Open), TestHelpers.createToken(CDTokenType.Angle_Close), TestHelpers.createToken(CDTokenType.Dash), TestHelpers.createToken(CDTokenType.Angle_Close),
                },
                TestHelpers.createClassTokenStream("b")
            );
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Aggregation);
        }

        [Test, Description("[a]++->[b]")]
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
            var tokens = TestHelpers.createAssociationTokenStream(sourceFrom, sourceTo, targetFrom, targetTo);

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

        private void checkMultiplicityKind(MultiplicityKind actualKind, string s, string debugMsg)
        {
            MultiplicityKind expectedKind = MultiplicityKind.None;
            int dummy;
            if (s == null)
                expectedKind = MultiplicityKind.None;
            else if (int.TryParse(s, out dummy))
                expectedKind = MultiplicityKind.SingleNumber;
            else if (s.Equals("*"))
                expectedKind = MultiplicityKind.Star;

            Assert.AreEqual(expectedKind, actualKind, debugMsg);
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

        [Test]
        public void t10_Parse_Field()
        {
            var tokens = new TokenStream() { TestHelpers.createToken("fieldName") };
            var field = new CDParser(tokens).parseField();
            Assert.IsNotNull(field, "should have parsed correctly");
            Assert.AreEqual("fieldName", field.Name, "name wrong");
        }

        [Test]
        public void t10_Parse_Field_With_Type()
        {
            var tokens = new TokenStream() { TestHelpers.createToken("fieldName2"), TestHelpers.createToken(":"), TestHelpers.createToken("int"), };
            var field = new CDParser(tokens).parseField();
            Assert.IsNotNull(field, "should have parsed correctly");
            Assert.AreEqual("fieldName2", field.Name, "name wrong");
            Assert.AreEqual("int", field.Type, "type wrong");
        }

        [Test]
        public void t11_Parse_Class_With_Field()
        {
            var tokens = TestHelpers.createClassTokenStream("className", TestHelpers.createFieldTokenStream("fieldName", "fieldType"));
            
            var c = new CDParser(tokens).parseClass();

            Assert.IsNotNull(c, "failed to parse the class");
            Assert.AreEqual("className", c.Name, "name wrong");
            Assert.IsNotNull(c.Fields, "fields MUST NOT be null");
            var l = c.Fields.ToList();
            Assert.AreEqual(1, l.Count, "There should be exactly one field");
            var f = l[0];
            Assert.AreEqual("fieldName", f.Name, "field name wrong");
            Assert.AreEqual("fieldType", f.Type, "field type wrong");
        }

        [Test]
        public void t11_Parse_Class_With_Multiple_Fields()
        {
            var tokens = TestHelpers.createClassTokenStream("className", 
                TestHelpers.combineStreams(
                    TestHelpers.createFieldTokenStream("fieldName", "fieldType"),
                    new TokenStream() { TestHelpers.createToken(CDTokenType.Comma) }, // TODO single-token streams.
                    TestHelpers.createFieldTokenStream("fieldName2", "fieldType2")
                )
            );

            var c = new CDParser(tokens).parseClass();

            Assert.IsNotNull(c, "failed to parse the class");
            Assert.AreEqual("className", c.Name, "name wrong");
            Assert.IsNotNull(c.Fields, "fields MUST NOT be null");
            var l = c.Fields.ToList();
            Assert.AreEqual(2, l.Count, "There should be exactly one field");
            var f = l[0];
            Assert.AreEqual("fieldName", f.Name, "field name wrong");
            Assert.AreEqual("fieldType", f.Type, "field type wrong");
            f = l[1];
            Assert.AreEqual("fieldName2", f.Name, "2nd field name wrong");
            Assert.AreEqual("fieldType2", f.Type, "2nd field type wrong");
        }

        [Test]
        public void t11_Parse_Class_With_Multiple_Fields_Missing_Comma()
        {
            var tokens = TestHelpers.createClassTokenStream("className",
                TestHelpers.combineStreams(
                    TestHelpers.createFieldTokenStream("fieldName", "fieldType"),
                    TestHelpers.createFieldTokenStream("fieldName2", "fieldType2")
                )
            );

            var c = new CDParser(tokens).parseClass();
            Assert.IsNull(c, "invalid tokenstream -> must not parse");
        }

        [Test]
        public void t11_Junk_At_End()
        {
            var tokens = TestHelpers.combineStreams(
                TestHelpers.createClassTokenStream("className"),
                new TokenStream() { TestHelpers.createToken("JunkAtEnd") }
            );

            var cd = new CDParser(tokens).parseClassDiagram();
            Assert.IsNull(cd, "invalid tokenstream -> must not parse to class diagram");
            var c = new CDParser(tokens).parseClass();
            Assert.IsNull(c, "invalid tokenstream -> must not parse to class");
        }


    }

}
