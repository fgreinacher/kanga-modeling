using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Compiler.ClassDiagrams.Model;
using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModeling.Compiler.ClassDiagrams
{
    
    /// <summary>
    /// LL(1) recursive descent parser for class diagram strings.
    /// </summary>
    class CDParser
    {

        #region model implementing classes

        private class Class : Model.IClass
        {
            public Class(string name)
            {
                // TODO null check?
                Name = name;
            }

            public string Name
            {
                get;
                private set;
            }
        }

        #endregion

        public CDParser(TokenStream tokens)
        {
            if (tokens == null) throw new ArgumentNullException("tokens");
            _tokens = tokens;
        }

        // [ ID ]
        public Model.IClass parseClass()
        {
            if (checkAndRemoveLiteral("["))
            {
                // TODO error
                return null;
            }

            // TODO must be identifier!
            var c = new Class(_tokens[0].Value);
            _tokens.RemoveAt(0);

            if (checkAndRemoveLiteral("]"))
            {
                // TODO error
                return null;
            }

            return c;
        }

        private bool checkAndRemoveLiteral(String token)
        {
            if (_tokens.Count == 0)
                return true;
            var t = _tokens[0]; 
            _tokens.RemoveAt(0);
            return !t.Value.Equals(token);
        }

        private readonly TokenStream _tokens;

    }

}
