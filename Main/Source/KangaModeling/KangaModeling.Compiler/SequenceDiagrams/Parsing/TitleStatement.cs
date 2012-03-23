using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class TitleStatement : Statement
    {
        public Token Title
        {
            get { return Arguments[0]; }
        }

        public TitleStatement(Token keyword, Token titleText) 
            : base(keyword, titleText)
        {
        }

        public override void Build(AstBuilder builder)
        {
            builder.SetTitle(Title.Value);
        }
    }
}