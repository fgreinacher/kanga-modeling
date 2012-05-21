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
                if (@class == null) throw new ArgumentNullException("@class");
                _classes.Add(@class);
            }

            public void Add(IAssociation assoc)
            {
                if (assoc == null) throw new ArgumentNullException("assoc");
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
            public Association(AssocInfo info, IClass source, IClass target)
            {
                Kind = info.Kind;
                Source = source; 
                Target = target;
                SourceMultiplicity = info.SourceMult;
                TargetMultiplicity = info.TargetMult;
            }

            public Multiplicity SourceMultiplicity
            {
                get;
                private set;
            }

            public Multiplicity TargetMultiplicity
            {
                get;
                private set;
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
                parseClassOrAssociation(cd);

                // either no more tokens OR comma, otherwise error
                if (_tokens.Count > 0)
                {
                    if (_tokens[0].TokenType != CDTokenType.Comma)
                    {
                        // error, expected comma
                        return null;
                    }

                    // consume comma
                    _tokens.RemoveAt(0);
                }
            }

            return cd;
        }

        // class [ assoc class ]
        private void parseClassOrAssociation(ClassDiagram cd)
        {
            // class
            var c = parseClass();
            // TODO what if c == null?
            cd.AddClass(c);

            // either there is an association afterwards or not
            // if there is none, then either this is the end of input
            // or a comma follows.
            var assoc = parseAssociation();
            if (assoc != null)
            {
                // assoc did parse, must be followed by class
                var c2 = parseClass();
                cd.AddClass(c2);

                cd.Add(new Association(assoc, c, c2));
            }
        }

        private class AssocInfo
        {
            public AssocInfo(AssociationKind k)
            {
                Kind = k;
            }
            public AssociationKind Kind;
            public Multiplicity SourceMult;
            public Multiplicity TargetMult;
        }

        // simple ones: "->" "<>->" "+->" "++->"
        private AssocInfo parseAssociation()
        {
            var sourceMult = parseMultiplicity();
            AssocInfo assocInfo = null;

            // TODO checks!!!
            if (_tokens.Count >= 2 && _tokens[0].TokenType == CDTokenType.Dash && _tokens[1].TokenType == CDTokenType.Angle_Close)
            {
                _tokens.RemoveRange(0, 2);
                assocInfo = new AssocInfo(AssociationKind.Directed);
            }
            if (_tokens.Count >= 4 && _tokens[0].TokenType == CDTokenType.Angle_Open && _tokens[1].TokenType == CDTokenType.Angle_Close && _tokens[2].TokenType == CDTokenType.Dash && _tokens[3].TokenType == CDTokenType.Angle_Close)
            {
                _tokens.RemoveRange(0, 4);
                assocInfo = new AssocInfo(AssociationKind.Aggregation);
            }
            if (_tokens.Count >= 3 && _tokens[0].TokenType == CDTokenType.Plus && _tokens[1].TokenType == CDTokenType.Dash && _tokens[2].TokenType == CDTokenType.Angle_Close)
            {
                _tokens.RemoveRange(0, 3);
                assocInfo = new AssocInfo(AssociationKind.Aggregation);
            }
            if (_tokens.Count >= 4 && _tokens[0].TokenType == CDTokenType.Plus && _tokens[1].TokenType == CDTokenType.Plus && _tokens[2].TokenType == CDTokenType.Dash && _tokens[3].TokenType == CDTokenType.Angle_Close)
            {
                _tokens.RemoveRange(0, 4);
                assocInfo = new AssocInfo(AssociationKind.Composition);
            }
            if (_tokens.Count >= 1 && _tokens[0].TokenType == CDTokenType.Dash)
            {
                _tokens.RemoveRange(0, 1);
                assocInfo = new AssocInfo(AssociationKind.Undirected);
            }

            var targetMult = parseMultiplicity();

            if (assocInfo != null)
            {
                assocInfo.SourceMult = sourceMult;
                assocInfo.TargetMult = targetMult;
            }

            return assocInfo;
        }

        private Multiplicity parseMultiplicity()
        {
            Multiplicity m = null;
            
            if (_tokens.Count >= 3 && _tokens[0].TokenType == CDTokenType.Number && _tokens[1].TokenType == CDTokenType.DotDot)
            {
                if (_tokens[2].TokenType == CDTokenType.Number)
                {
                    m = new Multiplicity(Multiplicity.Kind.SingleNumber, _tokens[0].Value, Multiplicity.Kind.SingleNumber, _tokens[2].Value);
                }
                else if (_tokens[2].TokenType == CDTokenType.Star)
                {
                    m = new Multiplicity(Multiplicity.Kind.SingleNumber, _tokens[0].Value, Multiplicity.Kind.Star, null);
                }
                else
                {
                    // TODO ??
                }
                _tokens.RemoveRange(0, 3);
            }

            if (_tokens.Count >= 1)
            {
                Multiplicity.Kind kind = Multiplicity.Kind.None;
                if (_tokens[0].TokenType == CDTokenType.Number)
                    kind = Multiplicity.Kind.SingleNumber;
                else if (_tokens[0].TokenType == CDTokenType.Star)
                    kind = Multiplicity.Kind.Star;
                //else
                //    ; // TODO?!

                if (kind != Multiplicity.Kind.None)
                {
                    m = new Multiplicity(kind, _tokens[0].Value, Multiplicity.Kind.None, null);
                    _tokens.RemoveRange(0, 1);
                }
            }

            return m;
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
            // TODO what if there is no token?
            //if (_tokens[0].TokenType != CDTokenType.Identifier)
            //{
            //    // error: expected identifier.
            //    return null;
            //}
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
