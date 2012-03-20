using KangaModeling.Compiler.SequenceDiagrams;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class UnknownStatementTest
    {
        [Test]
        public void UnknownStatementConstructorTest()
        {
            Token invalidToken = new Token(); // TODO: Initialize to an appropriate value
            UnknownStatement target = new UnknownStatement(invalidToken);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        public void BuildTest()
        {
            //Token invalidToken = new Token(); // TODO: Initialize to an appropriate value
            //UnknownStatement target = new UnknownStatement(invalidToken); // TODO: Initialize to an appropriate value
            //AstBuilder builder = null; // TODO: Initialize to an appropriate value
            //target.Build(builder);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
