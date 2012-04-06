using System;
using System.Collections.Generic;
using KangaModeling.Compiler.SequenceDiagrams.Model;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class ModelBuilder : IModelBuilder
    {
        private readonly SequenceDiagram m_Diagram;
        private readonly DiagramElementFactory m_ElementFactory;
        private readonly Queue<ModelError> m_Errors;
        private readonly Stack<InteractionOperand> m_OperandStack;

        public ModelBuilder(SequenceDiagram diagram, DiagramElementFactory elementFactory)
        {
            m_Diagram = diagram;
            m_ElementFactory = elementFactory;
            m_Errors = new Queue<ModelError>();
            m_OperandStack = new Stack<InteractionOperand>();
        //    m_OperandStack.Append(diagram.Content.InteractionOperand);
        }

        private InteractionOperand CurrentOperand
        {
            get { return m_OperandStack.Peek(); }
        }

        public SequenceDiagram Diagram
        {
            get { return m_Diagram; }
        }

        public IEnumerable<ModelError> Errors
        {
            get { return m_Errors; }
        }

        public virtual bool HasParticipant(string name)
        {
            return m_Diagram.Participants.Contains(name);
        }

        private bool TryGetParticipantByName(string name, out Participant participant)
        {
            return m_Diagram.Participants.TryGetValue(name, out participant);
        }

        public virtual void CreateParticipant(Token id, Token name)
        {
            Participant participant;
            if (TryGetParticipantByName(id.Value, out participant))
            {
                AddError(id, "Participant with this id already exists.");
            }
            else
            {
                m_Diagram.Participants.Add(m_ElementFactory.CreateParticipant(id, name));                
            }
        }

        private void AddSignal(Token source, Token target, Token signalName, SignalType signalType)
        {
            Participant sourceParticipant;
            if (!TryGetParticipantByName(source.Value, out sourceParticipant))
            {
                AddError(source, "No such participant");
                return;
            }

            Participant targetParticipant;
            if (!TryGetParticipantByName(target.Value, out targetParticipant))
            {
                AddError(target, "No such participant");
                return;
            }

            if (sourceParticipant.Equals(targetParticipant))
            {
                AddError(target, "Self signal is not supported currently.");
                return;
            }
            
            SignalElement signalElement = m_ElementFactory.CreateSignal(signalName.Value, sourceParticipant, targetParticipant, signalType);
            m_Diagram.Content.InteractionOperand.AddElement(signalElement);
        }


        public void AddCallSignal(Token source, Token target, Token name)
        {
            AddSignal(source, target, name, SignalType.Call);
        }


        public void AddReturnSignal(Token source, Token target, Token name)
        {
            AddSignal(source, target, name, SignalType.CallReturn);
        }

        public void SetTitle(Token title)
        {
            if (m_Diagram.Title!=null)
            {
                AddError(title, "Title is already set. Title can be set only once.");
            }
            m_Diagram.Title = title.Value;
        }

        public void AddError(Token invalidToken, string message)
        {
            ModelError error = new ModelError(invalidToken, message);
            m_Errors.Enqueue(error);
        }

        private void ChangeState(Token target, ActivationStatus state)
        {
            Participant targetParticipant;
            if (!TryGetParticipantByName(target.Value, out targetParticipant))
            {
                AddError(target, "No such participant");
                return;
            }

            LifelineStatusElement element = m_ElementFactory.CreateLifelineStatusElement(targetParticipant, state);
            m_Diagram.Content.InteractionOperand.AddElement(element);
        }

        public void Activate(Token target)
        {
            ChangeState(target, ActivationStatus.Activate);
        }

        public void Deactivate(Token target)
        {
            ChangeState(target, ActivationStatus.Deactivate);
        }

        public void StartOpt(Token guardExpression)
        {
            throw new NotImplementedException();
        }

        public void End()
        {
            throw new NotImplementedException();
        }
    }
}