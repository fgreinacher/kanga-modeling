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

        private class ClassDiagram : Model.IClassDiagram
        {
            public IEnumerable<IClass> Classes
            {
                get { return _classes; }
            }

            public IEnumerable<IAssociation> Associations
            {
                get { return _assocs; }
            }

            public void AddClass(IClass @class) {
                // TODO null check needed?
                _classes.Add(@class);
            }

            public void Add(IAssociation assoc)
            {
                // TODO null check needed?
                _assocs.Add(assoc);
            }

            private List<IClass> _classes = new List<IClass>();
            private List<IAssociation> _assocs = new List<IAssociation>();
        }

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

        private class Association : Model.IAssociation
        {
            public Association(AssociationKind kind, IClass source, IClass target)
            {
                Kind = kind;
                Source = source; 
                Target = target;
            }

            public string SourceRole
            {
                get;
                private set;
            }

            public string TargetRole
            {
                get;
                private set;
            }

            public AssociationKind Kind
            {
                get;
                private set;
            }

            public IClass Source
            {
                get;
                private set;
            }

            public IClass Target
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

        // assoc [ "," assoc ]* 
        public Model.IClassDiagram parseClassDiagram()
        {
            var cd = new ClassDiagram();

            while (_tokens.Count > 0)
            {
                // class
                var c = parseClass();
                // TODO what if c == null?
                cd.AddClass(c);


                // assoc token (lookahead)
                // TODO _tokens.Count == 0?
                if (_tokens.Count > 0 && isAssociation(_tokens[0].Value))
                {
                    var kind = getKind(_tokens[0].Value);
                    _tokens.RemoveAt(0);
                    var c2 = parseClass();
                    // TODO c2 == null?
                    cd.AddClass(c2);

                    cd.Add(new Association(kind, c, c2));
                }

                // at the end, there needs to be a comma
                if (_tokens.Count > 0 && checkAndRemoveLiteral(","))
                {
                    // TODO error
                    return null;
                }
            }

            return cd;
        }

        // TODO TokenKind!!!
        private bool isAssociation(string value)
        {
            return "->".Equals(value) || "-".Equals(value) || "<>->".Equals(value) || "++->".Equals(value) || "+->".Equals(value);
        }

        private AssociationKind getKind(string value)
        {
            AssociationKind kind = AssociationKind.Undirected;
            switch(value) {
                case "->": kind = AssociationKind.Directed; break;
                case "-": kind = AssociationKind.Undirected; break;
                case "+->": kind = AssociationKind.Aggregation; break;
                case "++->": kind = AssociationKind.Composition; break;
                case "<>->": kind = AssociationKind.Aggregation; break;
            }
            return kind;
        }

        // "[" ID "]"
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
