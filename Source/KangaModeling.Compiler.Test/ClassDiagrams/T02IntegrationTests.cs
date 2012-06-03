using System.Linq;
using KangaModeling.Compiler.ClassDiagrams;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.ClassDiagrams
{
    public class T02IntegrationTests
    {

        [TestCase("[a]", TestName = "[a]")]
        [TestCase("[a|field]", TestName = "[a|field]")]
        [TestCase("[a||method()]", TestName = "[a||method()]")]
        [TestCase("[a]->[b]", TestName = "[a]->[b]")]
        [TestCase("[a|field : int, field2 : bool]", TestName = "[a|field : int, field2 : bool]")]
        [TestCase("[a||-method(int a, bool b):DiagramCreationResult]", TestName = "[a||-method(int a, bool b):DiagramCreationResult]")]
        //[TestCase("", TestName = "")]
        public void T01CheckParseOK(string source)
        {
            var result = DiagramCreator.CreateFrom(source);
            Assert.IsNotNull(result.ClassDiagram, "failed to parse");
            Assert.AreEqual(0, result.Errors.Count(), "no errors expected");
        }

    }
}