using System;
using System.Collections;
using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    /// <summary>
    /// A InteractionOperand is one box inside a combined fragment.
    /// 
    /// It consists of the guard expression, determining when the elements are to be
    /// carried out.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
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