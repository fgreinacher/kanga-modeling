using System;
using System.Collections.Generic;
using System.Linq;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class CallSignalStatement : SignalStatement
    {
        public CallSignalStatement(Token keyword, Token source, Token target, Token name) 
            : base(keyword, source, target, name)
        {
        }

		protected override SignalType GetSignalType()
		{
			return SignalType.Call;
		}
    }
}