using KangaModeling.Compiler.SequenceDiagrams;
using Moq;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class EnsureParticipantStatementTest
    {
        [TestCase("A", true)]
        [TestCase("A", false)]
        public void Test(string name, bool found)
        {
            var nameToken = new Token(0, name.Length, name);
            ParticipantStatement target = new EnsureParticipantStatement(nameToken);
            var builderMock = new Mock<IModelBuilder>(MockBehavior.Strict);
            builderMock.Setup(builder => builder.EnsureParticipant(nameToken));
            if (!found)
            {
                builderMock.Setup(builder => builder.CreateParticipant(nameToken, nameToken));
            }

            target.Build(builderMock.Object);

            builderMock.VerifyAll();
        }
    }
}