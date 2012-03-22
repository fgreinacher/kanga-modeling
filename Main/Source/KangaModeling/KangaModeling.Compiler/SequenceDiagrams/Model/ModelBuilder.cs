using System;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    // TODO [Hanke} rename when resharper is installed.
    internal class AstBuilder
    {
        private readonly SequenceDiagram m_Diagram;

        public SequenceDiagram Diagram
        {
            get { return m_Diagram; }
        }

        public AstBuilder(SequenceDiagram diagram)
        {
            m_Diagram = diagram;
        }

        internal Participant FindParticipant(String name)
        {
            // TODO case sensitivenes?
            return m_Diagram.Participants.Find(p => p.Name == name);
        }

        internal void CreateParticipant(string participantName)
        {
            // TODO check for correct name
            // TODO check if there is already a participant with that name.
            m_Diagram.Participants.Add(new Participant(participantName));
        }

        internal void AddSignal(SignalElement se)
        {
            // TODO null check
            m_Diagram.Content[0].AddElement(se);
        }


        public void SetTitle(string title)
        {
            m_Diagram.Title = title;
        }

        public void AddError(Token invalidToken, string message)
        {
            //TODO Modify to write errors somwhere else
            Console.Error.WriteLine("Error: {0} at {1}", message, invalidToken.Start);
        }

    }
}