using System;
using System.Linq;
using KangaModeling.Compiler.ClassDiagrams;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.ClassDiagrams
{

    class T02IntegrationTests
    {
        public class TestData
        {
            public TestData(string source)
            {
                if (source == null) throw new ArgumentNullException("source");
                Source = source;
            }

            public readonly string Source;

            public override string ToString()
            {
                return Source;
            }
        }

        static object[] GoodCases = new object[]
                                               {
                                                   new TestData("[a]"),
                                                   new TestData("[a|field]"),
                                                   new TestData("[a||method()]"),
                                                   new TestData("[a]->[b]"),
                                                   new TestData("[a|field : int, field2 : bool]"),
                                                   new TestData("[a||-method(int a, bool b):DiagramCreationResult]"),
                                               };

        static object[] BadCases = new object[]
                                               {
                                                   new TestData("a]"),
                                                   new TestData("[a"),
                                                   new TestData("[a]]"),
                                               };

        [Test, TestCaseSource("GoodCases")]
        public void T01CheckParseOK(TestData data)
        {
            var result = DiagramCreator.CreateFrom(data.Source);
            Assert.IsNotNull(result.ClassDiagram, "failed to parse");
            Assert.AreEqual(0, result.Errors.Count(), "no errors expected");
        }

        [Test, TestCaseSource("BadCases")]
        public void T02CheckParseFail(TestData data)
        {
            var result = DiagramCreator.CreateFrom(data.Source);
            Assert.IsNotNull(result.ClassDiagram, "failed to parse");
            Assert.AreEqual(0, result.Errors.Count(), "no errors expected");
        }

    }
}