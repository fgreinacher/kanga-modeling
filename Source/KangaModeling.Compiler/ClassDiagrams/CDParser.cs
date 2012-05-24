using System;
using System.Collections.Generic;
using KangaModeling.Compiler.ClassDiagrams.Model;

namespace KangaModeling.Compiler.ClassDiagrams
{

    public static class DiagramCreator
    {
        /// <summary>
        /// Conveniently parse a string to a sequence diagram.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A sequence diagram parsed from the text. Never null.</returns>
        public static IClassDiagram CreateFrom(string text)
        {
            return new CDParser(new CDScanner().Parse(text)).ParseClassDiagram();
        }

    }
    
    /// <summary>
    /// LL(k) recursive descent parser for class diagram strings.
    /// </summary>
    class CDParser
    {

        #region model implementing classes

        private class ClassDiagram : IClassDiagram
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

            private readonly List<IClass> _classes = new List<IClass>();
            private readonly List<IAssociation> _assocs = new List<IAssociation>();
        }

        private class Class : IClass
        {
            private readonly List<IField> _fields;
            public Class(string name)
            {
                // TODO null check?
                Name = name;
                _fields = new List<IField>(2);
            }

            public void Add(IField field)
            {
                if (field == null) throw new ArgumentNullException("field");
                _fields.Add(field);
            }

            public IEnumerable<IField> Fields
            {
                get
                {
                    return _fields;
                }
            }

            public string Name
            {
                get;
                private set;
            }
        }

        private class Field : IField
        {
            public Field(string name, string type, VisibilityModifier visibilityModifier = VisibilityModifier.Public)
            {
                Name = name;
                Type = type;
                Visibility = visibilityModifier;
            }
            public string Name { get; private set; }
            public string Type { get; private set; }
            public VisibilityModifier Visibility { get; private set; }
        }

        private class Association : IAssociation
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

        private class Method : IMethod
        {
            public Method(string name, string returnType, IEnumerable<MethodParameter> parameters, VisibilityModifier visibility)
            {
                Name = name;
                ReturnType = returnType;
                Parameters = parameters;
                Visibility = visibility;
            }

            public string Name { get; private set; }
            public string ReturnType { get; private set; }
            public IEnumerable<MethodParameter> Parameters { get; private set; }
            public VisibilityModifier Visibility { get; private set; }
        }

        #endregion

        public CDParser(TokenStream tokens)
        {
            if (tokens == null) throw new ArgumentNullException("tokens");
            _tokens = tokens;
        }

        #region productions

        // assoc [ "," assoc ]* 
        public IClassDiagram ParseClassDiagram()
        {
            var cd = new ClassDiagram();

            while (_tokens.Count > 0)
            {
                if (!ParseClassOrAssociation(cd))
                    return null;

                // either no more tokens OR comma, otherwise error
                if (!_tokens.TryConsume(TokenType.Comma))
                    break;
            }

            if (_tokens.Count > 0)
            {
                // TODO ERROR - junk at end
                return null;
            }

            return cd;
        }

        // "[" ID "]"
        public IClass ParseClass()
        {
            if (!_tokens.TryConsume(TokenType.BracketOpen))
            {
                // TODO error
                return null;
            }

            // TODO must be identifier!
            // TODO what if there is no token?
            //if (_tokens[0].TokenType != TokenType.Identifier)
            //{
            //    // error: expected identifier.
            //    return null;
            //}
            CDToken token;
            if (!_tokens.TryConsume(TokenType.Identifier, out token))
            {
                // error
                return null;
            }
            var c = new Class(token.Value);

            // fields
            if(_tokens.TryConsume(TokenType.Pipe))
            {
                // TODO there can be 0 fields! "[a||method()]"
                IField field = null;
                do
                {
                    field = ParseField();
                    if(field != null)
                        c.Add(field); // TODO c == null?!

                    // remove "," if present
                    if(!_tokens.TryConsume(TokenType.Comma))
                        break;

                } while (field != null);

            }

            if (!_tokens.TryConsume(TokenType.BracketClose))
            {
                // TODO error
                return null;
            }

            return c;
        }

        // ID [ ":" ID ]
        public IField ParseField()
        {
            String name = null, type = null;
            CDToken token;
            VisibilityModifier vm;

            // handle visibility modifiers (OPTIONAL)
            TryConsumeVisibilityModifier(out vm);

            // TODO no field? "[classname|]"
            if(_tokens.TryConsume(TokenType.Identifier, out token))
                name = token.Value;
            // TODO else error

            if (_tokens.TryConsume(TokenType.Colon))
            {
                if (_tokens.TryConsume(TokenType.Identifier, out token))
                    type = token.Value;
                // TODO else ERROR
            }

            return name != null ? new Field(name, type, vm) : null;
        }

        public IMethod ParseMethod()
        {
            var vis = VisibilityModifier.Public;
            var name = string.Empty;
            string rettype = "void";
            CDToken token;

            // (optional) visibility
            TryConsumeVisibilityModifier(out vis);

            // (mandatory) name
            if(!_tokens.TryConsume(TokenType.Identifier, out token))
            {
                // TODO ERROR
                return null;
            }
            name = token.Value;

            // (mandatory) (
            if(!_tokens.TryConsume(TokenType.ParenthesisOpen))
            {
                // TODO error
                return null;
            }

            // method parameters
            var mp = new List<MethodParameter>();
            do
            {
                if (!_tokens.TryConsume(TokenType.Identifier, out token))
                    break; // no identifier -> finished
                var paramName = token.Value;

                if (!_tokens.TryConsume(TokenType.Identifier, out token))
                {
                    // TODO error; after method parameter name, the type!
                    break;
                }

                mp.Add(new MethodParameter(paramName, token.Value));
            } while (true);

            // (mandatory) )
            if (!_tokens.TryConsume(TokenType.ParenthesisClose))
            {
                // TODO error
                return null;
            }

            // (optional) return type
            if(_tokens.TryConsume(TokenType.Colon))
            {
                // now return type MUST follow
                if(!_tokens.TryConsume(TokenType.Identifier, out token))
                {
                    // TODO error!
                    return null;
                }

                rettype = token.Value;
            }

            return new Method(name, rettype, mp, vis);
        }

        #endregion

        private bool TryConsumeVisibilityModifier(out VisibilityModifier mod)
        {
            mod = VisibilityModifier.Public;
            if (_tokens.TryConsume(TokenType.Plus))
                mod = VisibilityModifier.Public;
            else if (_tokens.TryConsume(TokenType.Dash))
                mod = VisibilityModifier.Private;
            else if (_tokens.TryConsume(TokenType.Hash))
                mod = VisibilityModifier.Protected;
            else if (_tokens.TryConsume(TokenType.Tilde))
                mod = VisibilityModifier.Internal;
            else
                return false;

            return true;
        }

        // class [ assoc class ]
        private bool ParseClassOrAssociation(ClassDiagram cd)
        {
            // class
            var c = ParseClass();
            if (c == null)
            {
                // error must have been flagged by class parsing
                return false;
            }
            cd.AddClass(c);

            // either there is an association afterwards or not
            // if there is none, then either this is the end of input
            // or a comma follows.
            var assoc = ParseAssociation();
            if (assoc != null)
            {
                // assoc did parse, must be followed by class
                var c2 = ParseClass();
                cd.AddClass(c2);

                cd.Add(new Association(assoc, c, c2));
            }

            return true;
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
        private AssocInfo ParseAssociation()
        {
            var sourceMult = ParseMultiplicity();
            AssocInfo assocInfo = null;

            // TODO checks!!!
            if(_tokens.TryConsume(TokenType.Dash))
            {
                if(_tokens.TryConsume(TokenType.AngleClose))
                    assocInfo = new AssocInfo(AssociationKind.Directed);
                else
                    assocInfo = new AssocInfo(AssociationKind.Undirected);
            }
            if(_tokens.TryConsume(TokenType.AngleOpen, TokenType.AngleClose, TokenType.Dash, TokenType.AngleClose))
            {
                assocInfo = new AssocInfo(AssociationKind.Aggregation);
            }
            if (_tokens.TryConsume(TokenType.Plus))
            {
                if(_tokens.TryConsume(TokenType.Dash, TokenType.AngleClose))
                {
                    assocInfo = new AssocInfo(AssociationKind.Aggregation);
                }
                if(_tokens.TryConsume(TokenType.Plus, TokenType.Dash, TokenType.AngleClose))
                {
                    assocInfo = new AssocInfo(AssociationKind.Composition);
                }
            }

            var targetMult = ParseMultiplicity();

            if (assocInfo != null)
            {
                assocInfo.SourceMult = sourceMult;
                assocInfo.TargetMult = targetMult;
            }

            return assocInfo;
        }

        // TODO make public, provide specific tests.
        private Multiplicity ParseMultiplicity()
        {
            Multiplicity m = null;

            CDToken token1;
            if (_tokens.TryConsume(TokenType.Number, out token1))
            {
                if (_tokens.TryConsume(TokenType.DotDot))
                {
                    CDToken token2;
                    if (_tokens.TryConsume(TokenType.Number, out token2))
                    {
                        m = new Multiplicity(MultiplicityKind.SingleNumber, token1.Value, MultiplicityKind.SingleNumber, token2.Value);
                    }
                    else if (_tokens.TryConsume(TokenType.Star))
                    {
                        m = new Multiplicity(MultiplicityKind.SingleNumber, token1.Value, MultiplicityKind.Star, null);
                    }
                    // TODO else
                }
                else
                {
                    m = new Multiplicity(MultiplicityKind.SingleNumber, token1.Value, MultiplicityKind.None, null);
                }
            }
            else
            {
                CDToken token;
                if (_tokens.TryConsume(TokenType.Star, out token))
                    m = new Multiplicity(MultiplicityKind.Star, token.Value, MultiplicityKind.None, null);
            }

            return m;
        }

        private readonly TokenStream _tokens;

    }

}
