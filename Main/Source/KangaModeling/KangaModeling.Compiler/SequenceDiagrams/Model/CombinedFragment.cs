using System;
namespace KangaModeling.Compiler.Model
{
	
	/// <summary>
	/// A combined fragment encapsulates multiple calls between the participants inside a boundary.
	/// This can be used to model optional workflows, alternatives, loops, etc.
	/// 
	/// TODO combined fragments may be nested.
	/// </summary>
	public class CombinedFragment
	{
		public enum CFType {
			Root,
			Alternative,
		}
		
		public CombinedFragment ()
		{
		}
		
		public CFType Type { get; private set; }
	}
	
}

