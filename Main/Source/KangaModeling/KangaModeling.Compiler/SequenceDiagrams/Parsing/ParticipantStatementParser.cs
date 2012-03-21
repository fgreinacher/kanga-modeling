using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class ParticipantStatementParser : StatementParser
    {
        public const string Keyword = "participant";

        public const string AsKeyword = "as";
        //participant A 
        //participant Long Name as A
        public override IEnumerable<Statement> Parse(Scanner scanner)
        {
            Token keywordToken = scanner.ReadWord();
            scanner.SkipWhiteSpaces();
            Token descriptionToken = scanner.ReadToWord(AsKeyword);
            if (scanner.Eol)
            {
                yield return new SimpleParticipantStatement(keywordToken, descriptionToken);
            }

            scanner.ReadWord();
            scanner.SkipWhiteSpaces();
            Token nameToken = scanner.ReadToEnd();
            yield return new ExtendedParticipantStatement(keywordToken, nameToken, descriptionToken);
        }
    }
}
