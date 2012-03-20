using KangaModeling.Compiler.SequenceDiagrams._Ast;
using KangaModeling.Compiler.SequenceDiagrams._Scanner;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class TitleStatementTest
    {
        [Test]
        public void TitleStatementConstructorTest()
        {
            Token keyword = new Token(); // TODO: Initialize to an appropriate value
            Token argument = new Token(); // TODO: Initialize to an appropriate value
            TitleStatement target = new TitleStatement(keyword, argument);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        public void BuildTest()
        {
            //Token keyword = new Token(); // TODO: Initialize to an appropriate value
            //Token argument = new Token(); // TODO: Initialize to an appropriate value
            //TitleStatement target = new TitleStatement(keyword, argument); // TODO: Initialize to an appropriate value
            //AstBuilder builder = null; // TODO: Initialize to an appropriate value
            //target.Build(builder);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
