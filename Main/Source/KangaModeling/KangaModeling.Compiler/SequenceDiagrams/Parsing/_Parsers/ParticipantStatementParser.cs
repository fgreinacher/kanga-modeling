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
            Token nameToken = scanner.ReadTo(AsKeyword);
            if (nameToken.IsEmpty())
            {
                yield return new MissingArgumentStatement(keywordToken, nameToken);
                yield break;
            }
            if (scanner.Eol)
            {
                yield return new SimpleParticipantStatement(keywordToken, nameToken);
                yield break;
            }

            scanner.ReadWord();
            scanner.SkipWhiteSpaces();
            Token idToken = scanner.ReadToEnd();

            if (idToken.Length==0)
            {
                yield return new MissingArgumentStatement(keywordToken, idToken);
                yield break;
            }

            yield return new ParticipantStatement(keywordToken, idToken, nameToken);
        }
    }
}
