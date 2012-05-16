using KangaModeling.Compiler.SequenceDiagrams;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.Statements
{
    [TestFixture]
    public class UnknownStatementTest
    {
        [Test]
        public void UnknownStatementConstructorTest()
        {
            Token invalidToken = new Token(); 
            UnknownStatement target = new UnknownStatement(invalidToken);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        public void BuildTest()
        {
            //Token invalidToken = new Token(); 
            //UnknownStatement target = new UnknownStatement(invalidToken); 
            //ModelBuilder builder = null; 
            //target.Build(builder);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
