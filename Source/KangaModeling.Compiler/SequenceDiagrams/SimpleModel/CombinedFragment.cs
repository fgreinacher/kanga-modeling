using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class CombinedFragment : ICombinedFragment
    {
        private readonly Stack<Operand> m_Operands;
        private readonly OperatorType m_OperatorType;
        private readonly Operand m_Parent;
        private readonly Token m_Token;

        public CombinedFragment(Operand parent, OperatorType operatorType, Token token)
        {
            m_Parent = parent;
            m_OperatorType = operatorType;
            m_Token = token;
            m_Operands = new Stack<Operand>();
        }


        public Token Token
        {
            get { return m_Token; }
        }

        #region ICombinedFragment Members

        public IOperand Parent
        {
            get { return m_Parent; }
        }

        public OperatorType OperatorType
        {
            get { return m_OperatorType; }
        }

        public virtual string Title
        {
            get
            {
                switch (OperatorType)
                {
                    case OperatorType.Root:
                        return "sd";

                    case OperatorType.Opt:
                        return "opt";

                    case OperatorType.Alt:
                        return "alt";

                    case OperatorType.Loop:
                        return "loop";

                    default:
                        return string.Empty;
                }
            }
        }

        public IEnumerable<IOperand> Operands
        {
            get { return m_Operands; }
        }

        #endregion

        public void Add(Operand child)
        {
            m_Operands.Push(child);
        }

        public Operand LastOperand()
        {
            return m_Operands.Peek();
        }
    }
}