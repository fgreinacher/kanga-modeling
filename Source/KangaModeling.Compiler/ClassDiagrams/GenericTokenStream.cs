using System.Collections.Generic;

namespace KangaModeling.Compiler.ClassDiagrams
{
    // IDEA: classify a TokenStream to a subclass! needs to run once through all tokens.

    /// <summary>
    /// A stream of tokens; result of the scanner, input to the parser.
    /// </summary>
    /// TODO must not implement List;
    /// TODO must be lazy: cache some Tokens, call into scanner for more ( -> ANTLR )
    class GenericTokenStream : List<GenericToken>
    {
    }

}