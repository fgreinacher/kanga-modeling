using KangaModeling.Compiler.SequenceDiagrams.Ast;
using NUnit.Framework;
using KangaModeling.Compiler.SequenceDiagrams.Model;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class AstBuilderTest
    {
        [Test]
        public void AstBuilderConstructorTest()
        {
            var diagram = new SequenceDiagram();
            AstBuilder target = new AstBuilder(diagram);
            Assert.AreEqual(diagram, target.Diagram);
        }

        [Test]
        public void AddErrorTest()
        {
            Assert.Inconclusive("Expectations is unknown. The method must be implemented.");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Some Title")]
        public void SetTitleTest(string expectedTitle)
        {
            var diagram = new SequenceDiagram();
            AstBuilder target = new AstBuilder(diagram);
            target.SetTitle(expectedTitle);
            Assert.AreEqual(expectedTitle, diagram.Title);
        }
    }
}
