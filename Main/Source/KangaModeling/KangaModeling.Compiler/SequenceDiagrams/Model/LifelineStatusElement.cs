using System;

namespace KangaModeling.Compiler.SequenceDiagrams
{
	/// <summary>
	/// Represents a state change in the Lifeline of a participant.
	/// The state change can either "activate" or "deactivate" a Lifeline.
	/// Activated lifelines means that the participant is currently doing something.
	/// 
	/// Note that recursive activations are possible.
	/// </summary>
	public sealed class LifelineStatusElement : DiagramElement
	{
	    /// <summary>
		/// Initializes a new LifelineStatusElement and sets its fields.
		/// </summary>
		/// <param name="participant">The target whose activation status is changed.</param>
		/// <param name="activationStatus">The new Status.</param>
		public LifelineStatusElement(Participant participant, ActivationStatus activationStatus) {
			if(participant == null) throw new ArgumentNullException("participant");
			TargetParticipant = participant;
			ActivationStatus = activationStatus;
		}
		
		/// <summary>
		/// Gets the target participant (whose activation status is changed)
		/// </summary>
		public Participant TargetParticipant { get; private set; }
		
		/// <summary>
		/// Gets the (new) activation status of the target participant.
		/// </summary>
		public ActivationStatus ActivationStatus { get; private set; }
		
	}
}

