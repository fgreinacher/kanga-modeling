using System;
using System.Linq;
using NUnit.Framework;
using KangaModeling.Compiler.ClassDiagrams;
using KangaModeling.Compiler.ClassDiagrams.Model;

namespace KangaModeling.Compiler.Test.ClassDiagrams
{
    /// <summary>
    /// Tests for tokenizing a string with the class diagram scanner.
    /// </summary>
    [TestFixture]
    class T01ParserTests
    {

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void t00_Ctor_Throws_On_Null_Argument()
        {
            new CDParser(null);
        }

        [Test(Description="[ClassName]")]
        public void t01_Parse_Simple_Class()
        {
            var tokens = new TokenStream { TokenType.BracketOpen.Token(), "ClassName".Token(), TokenType.BracketClose.Token(), };
            var parser = new CDParser(tokens);
            var clazz = parser.ParseClass();
            Assert.AreEqual("ClassName", clazz.Name);
        }

        [Test(Description = "ClassName]")]
        public void t02_Parse_Simple_Class_Missing_Start_Bracket()
        {
            var tokens = new TokenStream { "ClassName".Token(), TokenType.BracketClose.Token(), };
            var clazz = new CDParser(tokens).ParseClass(); // TODO error handling?
            Assert.IsNull(clazz, "invalid");

            tokens = new TokenStream { "ClassName".Token(), TokenType.BracketClose.Token(), };
            var cd = new CDParser(tokens).ParseClassDiagram(); // TODO error handling?
            Assert.IsNull(cd, "invalid");
        }

        [Test(Description = "[ClassName")]
        public void t03_Parse_Simple_Class_Missing_End_Bracket()
        {
            var tokens = new TokenStream { TokenType.BracketOpen.Token(), "ClassName".Token(), };
            var clazz = new CDParser(tokens).ParseClass(); // TODO error handling?
            Assert.IsNull(clazz, "invalid");

            tokens = new TokenStream { TokenType.BracketOpen.Token(), "ClassName".Token(), };
            var cd = new CDParser(tokens).ParseClassDiagram(); // TODO error handling?
            Assert.IsNull(cd, "invalid");
        }

        [Test(Description = "")]
        public void t04_Parse_Simple_Class_No_Tokens()
        {
            var tokens = new TokenStream();
            var parser = new CDParser(tokens);
            var clazz = parser.ParseClass(); // TODO error handling?
            Assert.IsNull(clazz, "invalid");
        }

        [Test(Description="[a]")]
        public void t06_Parse_ClassDiagram_Containing_One_Class()
        {
            var tokens = TokenStreamBuilder.Class("a");

            var parser = new CDParser(tokens);
            var cd = parser.ParseClassDiagram();
            Assert.IsNotNull(cd, "parsing failed");

            var classes = cd.Classes.ToList();
            Assert.AreEqual(1, classes.Count, "wrong class count");
            Assert.AreEqual("a", classes[0].Name, "unexpected class name");
        }

        [Test, Description("[a],[b]")]
        public void t06_Parse_ClassDiagram_Containing_Two_Classes()
        {
            var tokens = new TokenStream { 
                TokenType.BracketOpen.Token(), "a".Token(), TokenType.BracketClose.Token(),
                TokenType.Comma.Token(),
                TokenType.BracketOpen.Token(), "b".Token(), TokenType.BracketClose.Token(),
            };
            var parser = new CDParser(tokens);
            var cd = parser.ParseClassDiagram();
            Assert.IsNotNull(cd, "parsing failed");

            var classes = cd.Classes.ToList();
            Assert.AreEqual(2, classes.Count, "wrong class count");
            Assert.AreEqual("a", classes[0].Name, "unexpected class name");
            Assert.AreEqual("b", classes[1].Name, "unexpected class name");
        }

        [Test, Description("[a]->[b]")]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Directed()
        {
            var tokens = new TokenStream { 
                TokenType.BracketOpen.Token(), "a".Token(), TokenType.BracketClose.Token(),
                TokenType.Dash.Token(), TokenType.AngleClose.Token(),
                TokenType.BracketOpen.Token(), "b".Token(), TokenType.BracketClose.Token(),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Directed);
        }

        [Test, Description("[a]-[b]")]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Undirected()
        {
            var tokens = new TokenStream { 
                TokenType.BracketOpen.Token(), "a".Token(), TokenType.BracketClose.Token(),
                TokenType.Dash.Token(),
                TokenType.BracketOpen.Token(), "b".Token(), TokenType.BracketClose.Token(),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Undirected);
        }

        [Test, Description("[a]+->[b]")]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Aggregation()
        {
            var tokens = new TokenStream { 
                TokenType.BracketOpen.Token(), "a".Token(), TokenType.BracketClose.Token(),
                TokenType.Plus.Token(), TokenType.Dash.Token(), TokenType.AngleClose.Token(),
                TokenType.BracketOpen.Token(), "b".Token(), TokenType.BracketClose.Token(),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Aggregation);
        }

        [Test, Description("[a]<>->[b]")]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Aggregation_Alternative()
        {
            var tokens = TokenStreamBuilder.CombineTokenStreams(
                TokenStreamBuilder.Class("a"),
                new TokenStream {  TokenType.AngleOpen.Token(), TokenType.AngleClose.Token(), TokenType.Dash.Token(), TokenType.AngleClose.Token(), },
                TokenStreamBuilder.Class("b")
            );
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Aggregation);
        }

        [Test, Description("[a]++->[b]")]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Composition()
        {
            var tokens = new TokenStream { 
                TokenType.BracketOpen.Token(), "a".Token(), TokenType.BracketClose.Token(),
                TokenType.Plus.Token(), TokenType.Plus.Token(), TokenType.Dash.Token(), TokenType.AngleClose.Token(),
                TokenType.BracketOpen.Token(), "b".Token(), TokenType.BracketClose.Token(),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Composition);
        }

        private void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(TokenStream tokens, AssociationKind expectedKind)
        {
            var parser = new CDParser(tokens);
            var cd = parser.ParseClassDiagram();
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
            var tokens = TokenStreamBuilder.Association(sourceFrom, sourceTo, targetFrom, targetTo);

            var parser = new CDParser(tokens);
            var cd = parser.ParseClassDiagram();
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
            var tokens = new TokenStream { 
                TokenType.BracketOpen.Token(), "a".Token(), TokenType.BracketClose.Token(),
                "-".Token(), "associationName".Token(), ">".Token(),
                TokenType.BracketOpen.Token(), "b".Token(), TokenType.BracketClose.Token(),
            };

            var parser = new CDParser(tokens);
            var cd = parser.ParseClassDiagram();
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
            var tokens = new TokenStream { "fieldName".Token() };
            var field = new CDParser(tokens).ParseField();
            Assert.IsNotNull(field, "should have parsed correctly");
            Assert.AreEqual("fieldName", field.Name, "name wrong");
        }

        [Test]
        public void t10_Parse_Field_With_Type()
        {
            var tokens = new TokenStream { "fieldName2".Token(), ":".Token(), "int".Token(), };
            var field = new CDParser(tokens).ParseField();
            Assert.IsNotNull(field, "should have parsed correctly");
            Assert.AreEqual("fieldName2", field.Name, "name wrong");
            Assert.AreEqual("int", field.Type, "type wrong");
        }

        [Test]
        public void t11_Parse_Class_With_Field()
        {
            var tokens = TokenStreamBuilder.Class("className", TokenStreamBuilder.Field("fieldName", "fieldType"));
            
            var c = new CDParser(tokens).ParseClass();

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
            var tokens = TokenStreamBuilder.Class("className", 
                TokenStreamBuilder.CombineTokenStreams(
                    TokenStreamBuilder.Field("fieldName", "fieldType"),
                    new TokenStream { TokenType.Comma.Token() }, // TODO single-token streams.
                    TokenStreamBuilder.Field("fieldName2", "fieldType2")
                )
            );

            var c = new CDParser(tokens).ParseClass();

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
            var tokens = TokenStreamBuilder.Class("className",
                TokenStreamBuilder.CombineTokenStreams(
                    TokenStreamBuilder.Field("fieldName", "fieldType"),
                    TokenStreamBuilder.Field("fieldName2", "fieldType2")
                )
            );

            var c = new CDParser(tokens).ParseClass();
            Assert.IsNull(c, "invalid tokenstream -> must not parse");
        }

        [Test]
        public void t12_Junk_At_End()
        {
            var tokens = TokenStreamBuilder.CombineTokenStreams(
                TokenStreamBuilder.Class("className"),
                new TokenStream { "JunkAtEnd".Token() }
            );

            var cd = new CDParser(tokens).ParseClassDiagram();
            Assert.IsNull(cd, "invalid tokenstream -> must not parse to class diagram");
            var c = new CDParser(tokens).ParseClass();
            Assert.IsNull(c, "invalid tokenstream -> must not parse to class");
        }

        [TestCase("+", VisibilityModifier.Public, TestName = "+ for Public")]
        [TestCase("-", VisibilityModifier.Private, TestName = "+ for Private")]
        [TestCase("#", VisibilityModifier.Protected, TestName = "+ for Protected")]
        [TestCase("~", VisibilityModifier.Internal, TestName = "~ for Internal")]
        [Test]
        public void t13_Parse_Field_With_AccessModifier(String vm, VisibilityModifier expectedVm)
        {
            var tokens = TokenStreamBuilder.Field("fieldName", "fieldType", vm);

            var f = new CDParser(tokens).ParseField();

            Assert.IsNotNull(f, "field parsing failed");
            Assert.AreEqual("fieldName", f.Name);
            Assert.AreEqual("fieldType", f.Type);
            Assert.AreEqual(expectedVm, f.Visibility, "wrong visibility modifier");
        }


    }

}
