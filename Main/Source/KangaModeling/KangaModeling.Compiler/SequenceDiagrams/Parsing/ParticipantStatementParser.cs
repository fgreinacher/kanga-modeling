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
            Token nameOrdescription = scanner.ReadToWord(AsKeyword);
            if (nameOrdescription.Length==0)
            {
                yield return new MissingArgumentStatement(keywordToken, nameOrdescription);
                yield break;
            }
            if (scanner.Eol)
            {
                yield return new SimpleParticipantStatement(keywordToken, nameOrdescription);
            }

            scanner.ReadWord();
            scanner.SkipWhiteSpaces();
            Token nameToken = scanner.ReadToEnd();
            yield return new ParticipantStatement(keywordToken, nameToken, nameOrdescription);
        }
    }
}
