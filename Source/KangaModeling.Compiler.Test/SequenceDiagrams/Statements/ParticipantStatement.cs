using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Compiler.SequenceDiagrams;
using Moq;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class ParticipantStatementTest
    {
        [TestCase("A", "Some Name")]
        public void Test(string id, string name)
        {
            var keyWordToken = new Token(0, 100, "participant");
            var idToken = new Token(0, id.Length, id);
            var nameToken = new Token(0,name.Length, name);
            ParticipantStatement target = new ParticipantStatement(keyWordToken, idToken, nameToken);
            var builderMock = new Mock<IModelBuilder>(MockBehavior.Strict, null, null);
            builderMock.Setup(builder => builder.CreateParticipant(idToken, nameToken));
            
            target.Build(builderMock.Object);
            
            builderMock.VerifyAll();
        }
    }
}


