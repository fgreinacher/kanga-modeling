using System;

namespace KangaModeling.Compiler.Model
{
	/// <summary>
	/// A Participant participates in a sequence diagram.
	/// It defines its own lifeline, can call other participants, and can receive calls.
	/// </summary>
	public class Participant
	{
		public Participant (String name)
		{
		}
		
		public String Name { get; private set; }
	}
	
}

