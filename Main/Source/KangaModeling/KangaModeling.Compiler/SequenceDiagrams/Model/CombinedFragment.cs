using System;
using System.Collections.Generic;
using System.Collections;

namespace KangaModeling.Compiler.SequenceDiagrams
{
	
	/// <summary>
	/// The interaction operator of a combined fragment.
	/// Think of this as the "type" of the combined fragment.
	/// </summary>
	public enum InteractionOperator {
		/// <summary>
		/// The root combined fragment type. 
		/// Only used for the invisible first combined fragment.
		/// </summary>
		Root,
		/// <summary>Alternative Combined Fragment</summary>
		Alternative,
		/// <summary>Loop Combined Fragment</summary>
		Loop,
		/// <summary>Parallel Combined Fragment</summary>
		Parallel,
	}
	
	/// <summary>
	/// A combined fragment encapsulates multiple calls between the participants inside a boundary.
	/// This can be used to model optional workflows, alternatives, loops, etc.
	/// </summary>
	public abstract class CombinedFragment : DiagramElement, IEnumerable<InteractionOperand>
	{
		
		/// <summary>
		/// Initializes a new AlternativeCombinedFragment and sets its fields.
		/// </summary>
		protected CombinedFragment(InteractionOperator op) {
			_compartments = new List<InteractionOperand>(2);
			Type = op;
		}
		
		/// <summary>
		/// Add a compartment to the combined fragment.
		/// </summary>
		/// <param name="guardExpression">The guard expression for this case. Must not be null.</param>
		/// <returns>The new compartment, where other DiagramElements can be added to. Never null.</returns>
		protected InteractionOperand CreateInteractionOperand(String guardExpression) {
			if(String.IsNullOrEmpty(guardExpression)) throw new ArgumentException("guardExpression");
			InteractionOperand c = new InteractionOperand(guardExpression);
			_compartments.Add(c);
			return c;
		}
		
		IEnumerator<InteractionOperand> IEnumerable<InteractionOperand>.GetEnumerator() {
			foreach(InteractionOperand de in _compartments)
				yield return de;
		}
		
		IEnumerator IEnumerable.GetEnumerator() {
			foreach(InteractionOperand de in _compartments)
				yield return de;
		}

        public InteractionOperand this[int index]
        {
            get
            {
                return _compartments[index];
            }
        }
		
		/// <summary>
		/// The interaction operands.
		/// </summary>
		private List<InteractionOperand> _compartments;
		
		/// <summary>
		/// The type of this combined fragment.
		/// </summary>
		public InteractionOperator Type { get; private set; }
		
	}

    /// <summary>
    /// The root combined fragment is the root of all content inside a sequence diagram.
    /// </summary>
    public sealed class RootCombinedFragment : CombinedFragment
    {
        /// <summary>
        /// The root has no guard expression per se, use a default one.
        /// </summary>
        public static readonly String DefaultGuardExpression = "Root";

        public RootCombinedFragment() : base(InteractionOperator.Root)
        {
            CreateInteractionOperand(DefaultGuardExpression);
        }

        /// <summary>
        /// Gets the single InteractionOperand of the root combined fragment.
        /// </summary>
        public InteractionOperand InteractionOperand
        {
            get
            {
                return this[0];
            }
        }

    }

	/// <summary>
	/// A InteractionOperand is one box inside a combined fragment.
	/// 
	/// It consists of the guard expression, determining when the elements are to be
	/// carried out.
	/// </summary>
	public sealed class InteractionOperand : IEnumerable<DiagramElement> {
		
		public InteractionOperand(String guardExpression) {
			// TODO check string
			_compartmentElements = new List<DiagramElement>();
			_guardExpression = guardExpression;
		}
		
		public void AddElement(DiagramElement de) {
			if(de == null) throw new ArgumentNullException("de");
			_compartmentElements.Add(de);
		}
		
		IEnumerator<DiagramElement> IEnumerable<DiagramElement>.GetEnumerator() {
			foreach(DiagramElement de in _compartmentElements)
				yield return de;
		}
		
		IEnumerator IEnumerable.GetEnumerator() {
			foreach(DiagramElement de in _compartmentElements)
				yield return de;
		}
		
		public String GuardExpression { get { return _guardExpression; } } 
		
		private readonly List<DiagramElement> _compartmentElements;
		private readonly String _guardExpression;
	}
}