using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    public interface ICombinedFragment
    {
        IOperand Parent { get; }
        OperatorType OperatorType { get; }
        string Title { get; }
        IEnumerable<IOperand> Operands { get; }
    }
}