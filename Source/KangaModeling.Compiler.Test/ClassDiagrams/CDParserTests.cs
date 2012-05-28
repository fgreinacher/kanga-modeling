﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using KangaModeling.Compiler.ClassDiagrams;
using KangaModeling.Compiler.ClassDiagrams.Model;

namespace KangaModeling.Compiler.Test.ClassDiagrams
{

    public class IntegrationTests
    {

        [TestCase("[a]", TestName = "[a]")]
        [TestCase("[a|field]", TestName = "[a|field]")]
        [TestCase("[a||method()]", TestName = "[a||method()]")]
        [TestCase("[a]->[b]", TestName = "[a]->[b]")]
        public void T01CheckParseOK(string source)
        {
            var result = DiagramCreator.CreateFrom(source);
            Assert.IsNotNull(result.ClassDiagram, "failed to parse");
            Assert.AreEqual(0, result.Errors.Count(), "no errors expected");
        }

    }

    /// <summary>
    /// Some extension on ClassDiagramTokenStream.
    /// </summary>
    static class ClassDiagramTokenStreamExtensions
    {
        internal static IClassDiagram ParseClassDiagram(this ClassDiagramTokenStream classDiagramTokenStream, ClassDiagramParser.ErrorCallback errorCallback = null)
        {
            return new ClassDiagramParser(classDiagramTokenStream, errorCallback).ParseClassDiagram();
        }

        internal static IClass ParseClass(this ClassDiagramTokenStream classDiagramTokenStream, ClassDiagramParser.ErrorCallback errorCallback = null)
        {
            return new ClassDiagramParser(classDiagramTokenStream, errorCallback).ParseClass();
        }

        internal static IMethod ParseMethod(this ClassDiagramTokenStream classDiagramTokenStream, ClassDiagramParser.ErrorCallback errorCallback = null)
        {
            return new ClassDiagramParser(classDiagramTokenStream, errorCallback).ParseMethod();
        }

        internal static IField ParseField(this ClassDiagramTokenStream classDiagramTokenStream, ClassDiagramParser.ErrorCallback errorCallback = null)
        {
            return new ClassDiagramParser(classDiagramTokenStream, errorCallback).ParseField();
        }
        internal static Multiplicity ParseMultiplicity(this ClassDiagramTokenStream classDiagramTokenStream, ClassDiagramParser.ErrorCallback errorCallback = null)
        {
            return new ClassDiagramParser(classDiagramTokenStream, errorCallback).ParseMultiplicity();
        }
    }

    /// <summary>
    /// Tests for tokenizing a string with the class diagram scanner.
    /// </summary>
    [TestFixture]
    class T01ParserTests
    {

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void t00_Ctor_Throws_On_Null_Argument()
        {
            new ClassDiagramParser(null);
        }

        [Test(Description="[ClassName]")]
        public void t01_Parse_Simple_Class()
        {
            var tokens = new ClassDiagramTokenStream { TokenType.BracketOpen.Token(), "ClassName".Token(), TokenType.BracketClose.Token(), };
            var parser = new ClassDiagramParser(tokens);
            var clazz = parser.ParseClass();
            Assert.AreEqual("ClassName", clazz.Name);
        }

        [Test(Description="[a]")]
        public void t06_Parse_ClassDiagram_Containing_One_Class()
        {
            var tokens = TokenStreamBuilder.Class("a");

            var parser = new ClassDiagramParser(tokens);
            var cd = parser.ParseClassDiagram();
            Assert.IsNotNull(cd, "parsing failed");

            var classes = cd.Classes.ToList();
            Assert.AreEqual(1, classes.Count, "wrong class count");
            Assert.AreEqual("a", classes[0].Name, "unexpected class name");
        }

        [Test, Description("[a],[b]")]
        public void t06_Parse_ClassDiagram_Containing_Two_Classes()
        {
            var tokens = new ClassDiagramTokenStream { 
                TokenType.BracketOpen.Token(), "a".Token(), TokenType.BracketClose.Token(),
                TokenType.Comma.Token(),
                TokenType.BracketOpen.Token(), "b".Token(), TokenType.BracketClose.Token(),
            };
            var parser = new ClassDiagramParser(tokens);
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
            var tokens = new ClassDiagramTokenStream { 
                TokenType.BracketOpen.Token(), "a".Token(), TokenType.BracketClose.Token(),
                TokenType.Dash.Token(), TokenType.AngleClose.Token(),
                TokenType.BracketOpen.Token(), "b".Token(), TokenType.BracketClose.Token(),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Directed);
        }

        [Test, Description("[a]-[b]")]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Undirected()
        {
            var tokens = new ClassDiagramTokenStream { 
                TokenType.BracketOpen.Token(), "a".Token(), TokenType.BracketClose.Token(),
                TokenType.Dash.Token(),
                TokenType.BracketOpen.Token(), "b".Token(), TokenType.BracketClose.Token(),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Undirected);
        }

        [Test, Description("[a]+->[b]")]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Aggregation()
        {
            var tokens = new ClassDiagramTokenStream { 
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
                new ClassDiagramTokenStream { TokenType.AngleOpen.Token(), TokenType.AngleClose.Token(), TokenType.Dash.Token(), TokenType.AngleClose.Token(), },
                TokenStreamBuilder.Class("b")
            );
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Aggregation);
        }

        [Test, Description("[a]++->[b]")]
        public void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes_Composition()
        {
            var tokens = new ClassDiagramTokenStream { 
                TokenType.BracketOpen.Token(), "a".Token(), TokenType.BracketClose.Token(),
                TokenType.Plus.Token(), TokenType.Plus.Token(), TokenType.Dash.Token(), TokenType.AngleClose.Token(),
                TokenType.BracketOpen.Token(), "b".Token(), TokenType.BracketClose.Token(),
            };
            t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(tokens, AssociationKind.Composition);
        }

        private void t07_Parse_ClassDiagram_Containing_Two_Associated_Classes(ClassDiagramTokenStream genericTokens, AssociationKind expectedKind)
        {
            var parser = new ClassDiagramParser(genericTokens);
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
            var tokens = new ClassDiagramTokenStream();
            tokens.AddRange(TokenStreamBuilder.Class("a"));

            tokens.AddRange(new[] { sourceFrom.Token() });
            if (sourceTo != null)
                tokens.AddRange(new[] { "..".Token(), sourceTo.Token() });

            tokens.AddRange(new[] { TokenType.Dash.Token() });

            tokens.AddRange(new[] { targetFrom.Token() });
            if (targetTo != null)
                tokens.AddRange(new[] { "..".Token(), targetTo.Token() });

            tokens.AddRange(TokenStreamBuilder.Class("b"));

            var parser = new ClassDiagramParser(tokens);
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
            var tokens = new ClassDiagramTokenStream { 
                TokenType.BracketOpen.Token(), "a".Token(), TokenType.BracketClose.Token(),
                "-".Token(), "associationName".Token(), ">".Token(),
                TokenType.BracketOpen.Token(), "b".Token(), TokenType.BracketClose.Token(),
            };

            var parser = new ClassDiagramParser(tokens);
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
            var tokens = new ClassDiagramTokenStream { "fieldName".Token() };
            var field = new ClassDiagramParser(tokens).ParseField();
            Assert.IsNotNull(field, "should have parsed correctly");
            Assert.AreEqual("fieldName", field.Name, "name wrong");
        }

        [Test]
        public void t10_Parse_Field_With_Type()
        {
            var tokens = new ClassDiagramTokenStream { "fieldName2".Token(), ":".Token(), "int".Token(), };
            var field = new ClassDiagramParser(tokens).ParseField();
            Assert.IsNotNull(field, "should have parsed correctly");
            Assert.AreEqual("fieldName2", field.Name, "name wrong");
            Assert.AreEqual("int", field.Type, "type wrong");
        }

        [Test]
        public void t11_Parse_Class_With_Field()
        {
            var tokens = TokenStreamBuilder.Class("className", TokenStreamBuilder.Field("fieldName", "fieldType"));
            
            var c = new ClassDiagramParser(tokens).ParseClass();

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
                    new ClassDiagramTokenStream { TokenType.Comma.Token() }, // TODO single-token streams.
                    TokenStreamBuilder.Field("fieldName2", "fieldType2")
                )
            );

            var c = new ClassDiagramParser(tokens).ParseClass();

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

        [TestCase("+", VisibilityModifier.Public, TestName = "+ for Public")]
        [TestCase("-", VisibilityModifier.Private, TestName = "+ for Private")]
        [TestCase("#", VisibilityModifier.Protected, TestName = "+ for Protected")]
        [TestCase("~", VisibilityModifier.Internal, TestName = "~ for Internal")]
        [Test]
        public void t13_Parse_Field_With_AccessModifier(String vm, VisibilityModifier expectedVm)
        {
            var tokens = TokenStreamBuilder.Field("fieldName", "fieldType", vm);

            var f = new ClassDiagramParser(tokens).ParseField();

            Assert.IsNotNull(f, "field parsing failed");
            Assert.AreEqual("fieldName", f.Name);
            Assert.AreEqual("fieldType", f.Type);
            Assert.AreEqual(expectedVm, f.Visibility, "wrong visibility modifier");
        }

        [Test]
        public void t14_Parse_Method()
        {
            var tokens = TokenStreamBuilder.Method("methodName");
            var m = new ClassDiagramParser(tokens).ParseMethod();
            AssertMethod(m, VisibilityModifier.Public, "methodName");
        }

        [TestCase("+", VisibilityModifier.Public, TestName = "+ for Public")]
        [TestCase("-", VisibilityModifier.Private, TestName = "+ for Private")]
        [TestCase("#", VisibilityModifier.Protected, TestName = "+ for Protected")]
        [TestCase("~", VisibilityModifier.Internal, TestName = "~ for Internal")]
        [Test]
        public void t14_Parse_Method_VisibilityModifier(string modifier, VisibilityModifier expectedVisibility)
        {
            var tokens = TokenStreamBuilder.Method("methodName", modifier);
            var m = new ClassDiagramParser(tokens).ParseMethod();
            Assert.AreEqual(expectedVisibility, m.Visibility, "unexpected visibility");
        }

        [Test]
        public void t14_Parse_Method_Parameter_WithType()
        {
            var parameterStream = new ClassDiagramTokenStream { "type".Token(), "parameter".Token() };
            var tokens = TokenStreamBuilder.Method("methodName", "+", parameterStream);
            
            var m = new ClassDiagramParser(tokens).ParseMethod();
            
            Assert.IsNotNull(m, "method parse error");
            var parameters = new List<MethodParameter>(m.Parameters);
            Assert.AreEqual(1, parameters.Count, "wrong parameter count");
            Assert.AreEqual("parameter", parameters[0].Name, "wrong name");
            Assert.AreEqual("type", parameters[0].Type, "wrong type");
        }

        [Test]
        public void t14_Parse_Method_Parameter_WithoutType()
        {
            var parameterStream = new ClassDiagramTokenStream { "parameter".Token() };
            var tokens = TokenStreamBuilder.Method("methodName", "+", parameterStream);

            var m = new ClassDiagramParser(tokens).ParseMethod();

            Assert.IsNotNull(m, "method parse error");
            var parameters = new List<MethodParameter>(m.Parameters);
            Assert.AreEqual(1, parameters.Count, "wrong parameter count");
            Assert.AreEqual("parameter", parameters[0].Name, "wrong name");
            Assert.AreEqual(string.Empty, parameters[0].Type, "wrong type");
        }

        [Test]
        public void t14_Parse_Method_ReturnType()
        {
            var tokens = TokenStreamBuilder.CombineTokenStreams(
                TokenStreamBuilder.Method("methodName"),
                TokenStreamBuilder.FromStrings(":", "returntype")
            );
            var m = new ClassDiagramParser(tokens).ParseMethod();

            Assert.IsNotNull(m, "method parse error");
            Assert.AreEqual("returntype", m.ReturnType);
        }

        [Test]
        public void t14_Parse_Class_With_Method()
        {
            // [className||-methodName(paramType paramName)]
            var tokens = TokenStreamBuilder.FromStrings
                ("[", "className", "|", "|", "-", "methodName", "(", "paramType", "paramName", ")", "]");

            var c = new ClassDiagramParser(tokens).ParseClass();

            Assert.IsNotNull(c, "class parse error");
        }

        [Test]
        public void t15_Parse_Three_Associated_Classes()
        {
            // [a]-[b]-[c]
            var tokens = TokenStreamBuilder.CombineTokenStreams(
                TokenStreamBuilder.Class("a"),
                TokenStreamBuilder.PureAssociation("-"),
                TokenStreamBuilder.Class("b"),
                TokenStreamBuilder.PureAssociation("-"),
                TokenStreamBuilder.Class("c")
            );

            var cd = new ClassDiagramParser(tokens).ParseClassDiagram();

            Assert.IsNotNull(cd, "class diagram1 parse error");
            Assert.AreEqual(3, cd.Classes.Count(), "wrong class count");
            Assert.AreEqual(2, cd.Associations.Count(), "wrong association count");
            // TODO more tests...
        }

        [Test]
        public void t16_Class_No_Fields_Just_Methods()
        {
            // [a]-[b]-[c]
            var tokens = TokenStreamBuilder.CombineTokenStreams(
                TokenStreamBuilder.FromStrings("[", "a", "|", "|"),
                TokenStreamBuilder.Method("methodName"),
                TokenStreamBuilder.FromStrings("]")
            );

            var c = new ClassDiagramParser(tokens).ParseClass();

            Assert.IsNotNull(c, "class parse error");
            Assert.AreEqual(0, c.Fields.Count(), "there should be no fields");
            Assert.AreEqual(1, c.Methods.Count(), "there should be one method");
            AssertMethod(c.Methods.ToArray()[0], VisibilityModifier.Public, "methodName");
        }

        internal class MultiplicityTestData : TestData
        {
            public MultiplicityTestData(Multiplicity expected, params string[] args) : base(ParseTarget.Multiplicity, args)
            {
                if (expected == null) throw new ArgumentNullException("expected");
                ExpectedMultiplicity = expected;
            }

            public Multiplicity ExpectedMultiplicity { get; private set; }
        }

        private static object[] MultiplicityTests = new[] {
            new MultiplicityTestData(new Multiplicity(MultiplicityKind.Star, "*", MultiplicityKind.None, string.Empty), "*"),
            new MultiplicityTestData(new Multiplicity(MultiplicityKind.SingleNumber, "0", MultiplicityKind.None, string.Empty), "0"),
            new MultiplicityTestData(new Multiplicity(MultiplicityKind.SingleNumber, "1", MultiplicityKind.None, string.Empty), "1"),
            new MultiplicityTestData(new Multiplicity(MultiplicityKind.SingleNumber, "134534534534523123122342343", MultiplicityKind.None, string.Empty), "134534534534523123122342343"),
            new MultiplicityTestData(new Multiplicity(MultiplicityKind.SingleNumber, "1", MultiplicityKind.SingleNumber, "2"), "1", "..", "2"),
            new MultiplicityTestData(new Multiplicity(MultiplicityKind.SingleNumber, "1", MultiplicityKind.Star, string.Empty), "1", "..", "*"),
            // new TestData(ParseTarget.Multiplicity, "*", "..", "1"), TODO LOGIC ERROR
        };

        [Test, TestCaseSource("MultiplicityTests")]
        public void t17_Multiplicity(TestData data)
        {
            var mult = TokenStreamBuilder.FromStrings(data.Arguments).ParseMultiplicity();
            Assert.IsNotNull(mult, "should have parsed");
        }

        #region infrastructure for error testing

        internal enum ParseTarget
        {
            Multiplicity,
            Field,
            Method,
            Class,
            ClassDiagram
        }

        internal class TestData
        {
            public TestData(ParseTarget target, params string[] args)
            {
                Arguments = args;
                Target = target;
            }
            public TestData(ParseTarget target)
            {
                Arguments = new string[] {};
                Target = target;
            }

            public readonly string[] Arguments;
            public readonly ParseTarget Target;

            public override string ToString()
            {
                if(Arguments.Length > 0)
                    return Target.ToString() + ": " + Arguments.Aggregate((s1, s2) => s1 + " " + s2);
                return Target.ToString() + ": <NO ARGUMENTS>";
            }
        }

        /// <summary>
        /// Checks that the given action (do some parsing) calls into the error callback.
        /// </summary>
        /// <param name="a"></param>
        private void ExpectError(Action<ClassDiagramParser.ErrorCallback> a)
        {
            bool called = false;
            ClassDiagramParser.ErrorCallback errorCallback = (expected, token) =>
            {
                called = true;
                return ClassDiagramParser.ErrorReturnCode.StopParsing;
            };

            a(errorCallback);

            Assert.IsTrue(called, "error callback has not been called");
        }

        #endregion

        private static object[] ErrorTests = new[] {
            new TestData(ParseTarget.ClassDiagram, "[", "a", "]", "-"), // missing association target
            new TestData(ParseTarget.ClassDiagram, "[", "classname", "]", "junk"), // junk at end

            new TestData(ParseTarget.Class, "classname"), // no brackets
            new TestData(ParseTarget.Class, "classname", "]"), // first bracket missing
            new TestData(ParseTarget.Class, "[", "]" ), // class name missing
            new TestData(ParseTarget.Class, "[", "classname" ), // last bracket missing
            // TODO new TestData(ParseTarget.Class, "[", "classname", "|", "]" ), // pipe but no field...
            new TestData(ParseTarget.Class ), // no tokens...
            new TestData(ParseTarget.Class, "[", "classname", "|", "field1", "field2", "]"), // comma missing between fields
            
            new TestData(ParseTarget.Method, ":", "(", ")"), // keyword as method name
            new TestData(ParseTarget.Method, "(", ")"), // no method name
            new TestData(ParseTarget.Method, "methodName", ")"), // missing start parenthesis
            new TestData(ParseTarget.Method, "methodName", "("), // missing end parenthesis
            new TestData(ParseTarget.Method, "methodName", "(", ")", ":"), // colon but no return type

            // TODO new TestData(ParseTarget.Field, "fieldName", ":"), // colon but no type
            new TestData(ParseTarget.Field, ":"), // keyword as field name

            // TODO new TestData(ParseTarget.Multiplicity, "..", "1"), // ".." but no source multiplicity
            // TODO new TestData(ParseTarget.Multiplicity, "1", ".."), // ".." but no target multiplicity
            // TODO new TestData(ParseTarget.Multiplicity, "identifier" ), // does not make sense

            // TODO new TestData(ParseTarget.Multiplicity, "2", "..", "1"), // LOGIC ERROR
        };

        [Test, TestCaseSource("ErrorTests")]
        public void t17_Errors(TestData data)
        {
            var tokens = TokenStreamBuilder.FromStrings(data.Arguments);
            var callback = RetrieveErrorCallback(data, tokens);
            ExpectError(callback);
        }

        private static Action<ClassDiagramParser.ErrorCallback> RetrieveErrorCallback(TestData data, ClassDiagramTokenStream tokens)
        {
            Action<ClassDiagramParser.ErrorCallback> callback = null;
            switch (data.Target)
            {
                case ParseTarget.ClassDiagram:
                    callback = ec => tokens.ParseClassDiagram(ec);
                    break;
                case ParseTarget.Class:
                    callback = ec => tokens.ParseClass(ec);
                    break;
                case ParseTarget.Method:
                    callback = ec => tokens.ParseMethod(ec);
                    break;
                case ParseTarget.Field:
                    callback = ec => tokens.ParseField(ec);
                    break;
                case ParseTarget.Multiplicity:
                    callback = ec => tokens.ParseMultiplicity(ec);
                    break;
                default:
                    throw new ArgumentException("don't know how to handle " + data.Target.ToString());
            }
            return callback;
        }

        private void AssertMethod(IMethod m, VisibilityModifier expectedVisibility, string expectedName, string expectedReturnType = "void")
        {
            Assert.IsNotNull(m, "unexpected null");
            Assert.AreEqual(expectedVisibility, m.Visibility, "unexpected method visiblity");
            Assert.AreEqual(expectedName, m.Name, "unexpected method name");
            Assert.AreEqual(expectedReturnType, m.ReturnType, "unexpected return type");
        }

    }

}
