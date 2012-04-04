using System;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    /// <summary>
    /// The root combined fragment is the root of all content inside a sequence diagram.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
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
}