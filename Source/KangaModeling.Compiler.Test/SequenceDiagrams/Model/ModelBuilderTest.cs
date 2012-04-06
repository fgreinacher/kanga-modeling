using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class ModelBuilderTest
    {
        [Test]
        public void AstBuilderConstructorTest()
        {
            var diagram = new SequenceDiagram();
            ModelBuilder target = new ModelBuilder(diagram, null);
            Assert.AreEqual(diagram, target.Diagram);
        }

        [Test]
        public void AddErrorTest()
        {
            Assert.Inconclusive("Expectations is unknown. The method must be implemented.");
        }

        [TestCase("")]
        [TestCase("Some Title")]
        public void SetTitleTest(string expectedTitle)
        {
            var diagram = new SequenceDiagram();
            IModelBuilder target = new ModelBuilder(diagram, null);
            target.SetTitle(new Token(20,20, expectedTitle));
            Assert.AreEqual(expectedTitle, diagram.Title);
        }

        [Test]
        public void SetTitleTwiceGeneratesError()
        {
            var diagram = new SequenceDiagram();
            IModelBuilder target = new ModelBuilder(diagram, null);
            int initialErrorCount = target.Errors.Count(); 
            target.SetTitle(new Token(20, 20, "SomeText"));
            target.SetTitle(new Token(20, 20, "SomeText"));
            int actualErrorCount = target.Errors.Count();
            Assert.AreEqual(1, actualErrorCount-initialErrorCount);
        }
    }
}
