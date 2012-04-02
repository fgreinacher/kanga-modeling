using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.Model;
using Moq;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class EnsureParticipantStatementTest
    {
        [TestCase("A", true)]
        [TestCase("A", false)]
        public void Tets(string name, bool found)
        {
            var nameToken = new Token(0, name.Length, name);
            ParticipantStatement target = new EnsureParticipantStatement(nameToken);
            var builderMock = new Mock<ModelBuilder>(MockBehavior.Strict, null, null);
            builderMock.Setup(builder => builder.HasParticipant(nameToken.Value)).Returns(found);
            if (!found)
            {
                builderMock.Setup(builder => builder.CreateParticipant(nameToken, nameToken));
            }

            target.Build(builderMock.Object);

            builderMock.VerifyAll();
        }
    }
}