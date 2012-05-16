using KangaModeling.Compiler.SequenceDiagrams;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.Statements
{
    [TestFixture]
    public class TitleStatementTest
    {
        [Test]
        public void TitleStatementConstructorTest()
        {
            Token keyword = new Token(); 
            Token argument = new Token(); 
            TitleStatement target = new TitleStatement(keyword, argument);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        public void BuildTest()
        {
            //Token keyword = new Token(); 
            //Token argument = new Token(); 
            //TitleStatement target = new TitleStatement(keyword, argument); 
            //ModelBuilder builder = null; 
            //target.Build(builder);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
