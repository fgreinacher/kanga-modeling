using System;

namespace KangaModeling.Compiler.SequenceDiagrams
{
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