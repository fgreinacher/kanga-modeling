﻿using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class ElseStatementParser : OneArgumentStatementParser
    {
        public const string ElseKeyword = "else";

        protected override Statement CreateEmptyStatement(Token keyword, Token emptyArgument)
        {
            return new ElseStatement(keyword, emptyArgument);
        }
        
        protected override Statement CreateStatement(Token keyword, Token argument)
        {
            return new ElseStatement(keyword, argument);
        }
    }
}