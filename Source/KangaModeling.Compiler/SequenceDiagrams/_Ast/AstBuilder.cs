using KangaModeling.Compiler.SequenceDiagrams._Scanner;

namespace KangaModeling.Compiler.SequenceDiagrams._Ast
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

        public void AddError(Token invalidToken, string text)
        {

        }
    }
}