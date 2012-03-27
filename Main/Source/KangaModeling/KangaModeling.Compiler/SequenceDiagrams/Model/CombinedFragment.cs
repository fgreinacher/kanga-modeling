using System;
using System.Collections.Generic;
using System.Collections;

namespace KangaModeling.Compiler.SequenceDiagrams
{
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
			Operator = op;
		}
		
		/// <summary>
		/// Add a compartment to the combined fragment.
		/// </summary>
		/// <param name="guardExpression">The guard expression for this case. Must not be null.</param>
		/// <returns>The new compartment, where other DiagramElements can be added to. Never null.</returns>
		protected InteractionOperand CreateInteractionOperand(String guardExpression) {
		    if (guardExpression == null)
		    {
		        throw new ArgumentNullException("guardExpression");
		    }
		    if(guardExpression.Length==0)
			{
			    throw new ArgumentException("Argument can not be empty.", "guardExpression");
			}
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
		public InteractionOperator Operator { get; private set; }
		
	}
}