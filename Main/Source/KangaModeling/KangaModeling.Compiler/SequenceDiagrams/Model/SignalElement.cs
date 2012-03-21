using System;
namespace KangaModeling.Compiler.SequenceDiagrams
{
	
	/// <summary>
	/// A SignalElement represents a signal or a call from one participant to another.
	/// 
	/// TODO there are signals coming from "nowhere" or going to "nirvana", omit these for now.
	/// TODO recursive calls for now prohibited.
	/// </summary>
	public sealed class SignalElement : DiagramElement
	{
		
		/// <summary>
		/// The type of signal.
		/// Allows the user to distinguish between different interactions between participants.
		/// </summary>
		public enum Type {
			/// <summary>A asynchronous signal</summary>
			Signal,
			/// <summary>A synchronous call</summary>
			Call,
			/// <summary>A return from a synchronous call</summary>
			CallReturn,
		}
		
		/// <summary>
		/// Initializes a new SignalElement and sets its fields.
		/// Attention: recursive signals are not allowed ATM.
		/// </summary>
		/// <param name="source">The source participant. Must not be null.</param>
		/// <param name="target">The target participant. Must not be null.</param>
		/// <param name="signalType">The type of signal.</param>
		public SignalElement(Participant source, Participant target, Type signalType) {
			if(source == null) throw new ArgumentNullException("source");
			if(target == null) throw new ArgumentNullException("target");
			
			if(source == target) throw new ArgumentException("recursive signals not supported ATM");
			
			SourceParticipant = source;
			TargetParticipant = target;
			SignalType = signalType;
		}
		
		/// <summary>
		/// The source of the signal. Never null.
		/// </summary>
		public Participant SourceParticipant { get; private set; }
		
		/// <summary>
		/// The target of the signal. Never null.
		/// </summary>
		public Participant TargetParticipant { get; private set; }
		
		/// <summary>
		/// The type of the signal.
		/// </summary>
		public Type SignalType { get; private set; }
	}
	
}

