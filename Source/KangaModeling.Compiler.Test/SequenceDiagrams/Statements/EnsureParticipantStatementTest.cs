using KangaModeling.Compiler.SequenceDiagrams;
using Moq;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class EnsureParticipantStatementTest
    {

        [TestCase("A Participant")]
        public void Ensuring_a_participant_must_tell_the_builder(string name)
        {
            var nameToken = new Token(0, name.Length, name);
            var target = new EnsureParticipantStatement(nameToken);
            var builderMock = new Mock<IModelBuilder>(MockBehavior.Strict);
            builderMock.Setup(builder => builder.EnsureParticipant(nameToken));

            target.Build(builderMock.Object);

            builderMock.VerifyAll();
        }

    }
}