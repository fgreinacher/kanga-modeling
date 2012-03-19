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
			
			/// <summary>
			/// The root combined fragment type. 
			/// Only used for the invisible first combined fragment.
			/// </summary>
			Root,
			
			/// <summary>
			/// Indicates the alternative combined fragment.
			/// </summary>
			Alternative,
			
		}

		/// <summary>
		/// The type of this combined fragment.
		/// </summary>
		public CFType Type { get; private set; }
		
	}
	
}

