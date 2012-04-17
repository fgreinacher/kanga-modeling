using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    public interface ICombinedFragment
    {
        IOperand Parent { get; }
        OperatorType OperatorType { get; }
        string Title { get; }
        IEnumerable<IOperand> Operands { get; }
    }
}