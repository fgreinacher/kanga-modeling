using KangaModeling.Compiler.SequenceDiagrams._Ast;
using KangaModeling.Compiler.SequenceDiagrams._Scanner;

namespace KangaModeling.Compiler.SequenceDiagrams._Parsing
{
    internal class ParticipantStatementParser : StatementParser
    {
        public const string Keyword = "participant";

        public const string AsKeyword = "as";
        //participant A 
        //participant Long Name as A
        public override Statement Parse(Scanner scanner)
        {
            Token keywordToken = scanner.ReadWord();
            scanner.SkipWhiteSpaces();
            Token descriptionToken = scanner.ReadTo(AsKeyword);
            if (scanner.Eol)
            {
                return new SimpleParticipantStatement(keywordToken, descriptionToken);
            }

            scanner.ReadWord();
            scanner.SkipWhiteSpaces();
            Token nameToken = scanner.ReadToEnd();
            return new ExtendedParticipantStatement(keywordToken, nameToken, descriptionToken);
        }
    }
}
