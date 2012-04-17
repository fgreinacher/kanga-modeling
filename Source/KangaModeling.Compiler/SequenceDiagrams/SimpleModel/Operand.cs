﻿using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class Operand : IOperand
    {
        private readonly Stack<IActivity> m_Activities;
        private readonly Stack<ICombinedFragment> m_Children;
        private readonly string m_GuardExpression;
        private readonly CombinedFragment m_Parent;
        private readonly Stack<ISignal> m_Signals;

        public Operand(CombinedFragment parent, string guardExpression)
        {
            m_Parent = parent;
            m_GuardExpression = guardExpression;
            m_Activities = new Stack<IActivity>();
            m_Signals = new Stack<ISignal>();
            m_Children = new Stack<ICombinedFragment>();
        }

        #region IOperand Members

        public ICombinedFragment Parent
        {
            get { return m_Parent; }
        }

        public string GuardExpression
        {
            get { return m_GuardExpression; }
        }

        public IEnumerable<IActivity> Activities
        {
            get { return m_Activities; }
        }

        public IEnumerable<ISignal> Signals
        {
            get { return m_Signals; }
        }

        public IEnumerable<ICombinedFragment> Children
        {
            get { return m_Children; }
        }

        #endregion

        public void Add(ISignal signal)
        {
            m_Signals.Push(signal);
        }

        public void Add(IActivity activity)
        {
            m_Activities.Push(activity);
        }

        public void Add(ICombinedFragment child)
        {
            m_Children.Push(child);
        }
    }
}