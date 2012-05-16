using KangaModeling.Compiler.SequenceDiagrams;
using Moq;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.Statements
{
    [TestFixture]
    public class SimleParticipantStatementTest
    {
        [TestCase("A")]
        public void Tets(string name)
        {
            var keywordToken = new Token(0, 100, "participant");
            var nameToken = new Token(0,name.Length, name);
            ParticipantStatement target = new SimpleParticipantStatement(keywordToken, nameToken);
            var builderMock = new Mock<IModelBuilder>(MockBehavior.Strict);
            builderMock.Setup(builder => builder.CreateParticipant(nameToken, nameToken, true));
            
            target.Build(builderMock.Object);
            
            builderMock.VerifyAll();
        }
    }
}