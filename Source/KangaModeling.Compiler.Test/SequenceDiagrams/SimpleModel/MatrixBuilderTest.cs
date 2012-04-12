using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.SimpleModel
{
    [TestFixture]
    public class MatrixBuilderTest
    {
      
        [Test]
        public void ActivateTest()
        {
            Matrix matrix = null; 
            var target = new MatrixBuilder(matrix); 
            var targetToken = new Token(); 
            target.Activate(targetToken);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void AddCallSignalTest()
        {
            Matrix matrix = null; 
            var target = new MatrixBuilder(matrix); 
            var source = new Token(); 
            var target1 = new Token(); 
            var name = new Token(); 
            target.AddCallSignal(source, target1, name);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void AddErrorTest()
        {
            Matrix matrix = null; 
            var target = new MatrixBuilder(matrix); 
            var invalidToken = new Token(); 
            string message = string.Empty; 
            target.AddError(invalidToken, message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void AddReturnSignalTest()
        {
            Matrix matrix = null; 
            var target = new MatrixBuilder(matrix); 
            var source = new Token(); 
            var target1 = new Token(); 
            var name = new Token(); 
            target.AddReturnSignal(source, target1, name);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        
        public void AddSignalTest()
        {
            //PrivateObject param0 = null; 
            //var target = new MatrixBuilder_Accessor(param0); 
            //var sourceToken = new Token(); 
            //var targetToken = new Token(); 
            //Signal_Accessor signal = null; 
            //target.AddSignal(sourceToken, targetToken, signal);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void CreateParticipantTest()
        {
            Matrix matrix = null; 
            var target = new MatrixBuilder(matrix); 
            var id = new Token(); 
            var name = new Token(); 
            target.CreateParticipant(id, name);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void DeactivateTest()
        {
            Matrix matrix = null; 
            var target = new MatrixBuilder(matrix); 
            var targetToken = new Token(); 
            target.Deactivate(targetToken);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void EndTest()
        {
            Matrix matrix = null; 
            var target = new MatrixBuilder(matrix); 
            target.End();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void ErrorsTest()
        {
            Matrix matrix = null; 
            var target = new MatrixBuilder(matrix); 
            IEnumerable<ModelError> actual;
            actual = target.Errors;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void FlushTest()
        {
            Matrix matrix = null; 
            var target = new MatrixBuilder(matrix); 
            target.Flush();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void HasParticipantTest()
        {
            Matrix matrix = null; 
            var target = new MatrixBuilder(matrix); 
            string name = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = target.HasParticipant(name);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void MatrixBuilderConstructorTest()
        {
            Matrix matrix = null; 
            var target = new MatrixBuilder(matrix);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [Test]
        
        public void MatrixTest()
        {
            //PrivateObject param0 = null; 
            //var target = new MatrixBuilder_Accessor(param0); 
            //Matrix_Accessor expected = null; 
            //Matrix_Accessor actual;
            //target.Matrix = expected;
            //actual = target.Matrix;
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void SetTitleTest()
        {
            Matrix matrix = null; 
            var target = new MatrixBuilder(matrix); 
            var title = new Token(); 
            target.SetTitle(title);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        public void StartOptTest()
        {
            Matrix matrix = null; 
            var target = new MatrixBuilder(matrix); 
            var guardExpression = new Token(); 
            target.StartOpt(guardExpression);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [Test]
        
        public void TryGetColumnByIdTest()
        {
            //PrivateObject param0 = null; 
            //var target = new MatrixBuilder_Accessor(param0); 
            //string name = string.Empty; 
            //Lifeline_Accessor lifeline = null; 
            //Lifeline_Accessor lifelineExpected = null; 
            //bool expected = false; 
            //bool actual;
            //actual = target.TryGetColumnById(name, out lifeline);
            //Assert.AreEqual(lifelineExpected, lifeline);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}