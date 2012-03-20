using System;
using KangaModeling.Compiler.SequenceDiagrams.Model;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test
{
	
	/// <summary>
	/// Contains all tests concerning the model of sequence diagrams.
	/// </summary>
	[TestFixture]
	public class ModelTests
	{
		
		[Test]
		public void t01_CheckSetTitle ()
		{
			// Setup
			SequenceDiagram sd = new SequenceDiagram();
			// Exercise
			sd.Title = "TestTitle";
			// Check
			Assert.AreEqual("TestTitle", sd.Title, "title not set correctly");
		}
		
		[Test]
		public void t10_CheckAddParticipant() 
		{
			// Setup
			SequenceDiagram sd = new SequenceDiagram();
			// Exercise
			sd.Participants.Add(new Participant("Player"));
			// Check
			Assert.AreEqual(1, sd.Participants.Count, "wrong count of participants");
			
			// TODO separate check for these?
			Assert.AreEqual("Player", sd.Participants[0].Name, "participant has wrong name");
		}
		
		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void t11_CheckNameOfParticipantMustBeNonNull() 
		{
			// Setup
			SequenceDiagram sd = new SequenceDiagram();
			// Exercise && Check
			sd.Participants.Add(new Participant(null));
		}
		
		[Test, ExpectedException(typeof(ArgumentException))]
		public void t12_CheckNameOfParticipantMustBeNotEmpty() 
		{
			// Setup
			SequenceDiagram sd = new SequenceDiagram();
			// Exercise && Check
			sd.Participants.Add(new Participant(String.Empty));
		}
		
		[Test]
		public void t11_CheckRemoveParticipant() 
		{
			// Setup
			SequenceDiagram sd = new SequenceDiagram();
			Participant p = new Participant("Player");
			sd.Participants.Add(p);
			// Exercise
			sd.Participants.Remove(p); // TODO remove by Participant object?
			// Check
			Assert.AreEqual(0, sd.Participants.Count, "after deleting the only participants, there must be none left");
		}
		
		[Test]
		public void t20_CheckDefaultContent() 
		{
			// Setup && Exercise
			SequenceDiagram sd = new SequenceDiagram();
			// Check
			Assert.IsNotNull(sd.Content, "after creating a diagram, the Content must be non-null");
			// TODO this is no real test, because the Content element has static type "CombinedFragment", and is non-null...
			Assert.IsInstanceOf(typeof(CombinedFragment), sd.Content, "The Content element has the wrong type");
			Assert.AreEqual(CombinedFragment.CFType.Root, sd.Content.Type, "The default combined fragment must have root type.");
			// TODO the root CombinedFragment must not have an attached msg! or semantics?
		}
		
	}
}

