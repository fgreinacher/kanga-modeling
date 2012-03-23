using System;
using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    // TODO [Hanke} rename when resharper is installed.
    internal class AstBuilder
    {
        private readonly SequenceDiagram m_Diagram;
        private readonly Queue<AstError> m_Errors;

        public AstBuilder(SequenceDiagram diagram)
        {
            m_Diagram = diagram;
            m_Errors = new Queue<AstError>();
        }

        public SequenceDiagram Diagram
        {
            get { return m_Diagram; }
        }

        public IEnumerable<AstError> Errors
        {
            get { return m_Errors; }
        }

        internal Participant FindParticipant(String name)
        {
            // TODO case sensitivenes?
            return m_Diagram.Participants.Find(p => p.Name == name);
        }

        internal void CreateParticipant(Token name, Token description)
        {
            // TODO check for correct name
            if (FindParticipant(name.Value) != null)
            {
                AddError(name, "Participant with this name already exists.");
            }
            else
            {
                m_Diagram.Participants.Add(new Participant(name.Value));                
            }
        }

        internal void AddSignal(SignalElement se)
        {
            // TODO null check
            m_Diagram.Content.InteractionOperand.AddElement(se);
        }


        public void SetTitle(string title)
        {
            m_Diagram.Title = title;
        }

        public void AddError(Token invalidToken, string message)
        {
            AstError error = new AstError(invalidToken, message);
            m_Errors.Enqueue(error);
        }

    }
}