using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Compiler.ClassDiagrams.Model;
using KangaModeling.Compiler.SequenceDiagrams;

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
            return new CDParser(new CDScanner().parse(text)).parseClassDiagram();
        }

    }
    
    /// <summary>
    /// LL(k) recursive descent parser for class diagram strings.
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
            private List<IField> _fields;
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

        private class Field : Model.IField
        {
            public Field(string name, string type)
            {
                Name = name;
                Type = type;
            }
            public string Name { get; private set; }
            public string Type { get; private set; }
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

        #region productions

        // assoc [ "," assoc ]* 
        public Model.IClassDiagram parseClassDiagram()
        {
            var cd = new ClassDiagram();

            while (_tokens.Count > 0)
            {
                if (!parseClassOrAssociation(cd))
                    return null;

                // either no more tokens OR comma, otherwise error
                if (!_tokens.TryConsume(CDTokenType.Comma))
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
        public Model.IClass parseClass()
        {
            if (!_tokens.TryConsume(CDTokenType.Bracket_Open))
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
            CDToken token = null;
            if (!_tokens.TryConsume(CDTokenType.Identifier, out token))
            {
                // error
                return null;
            }
            var c = new Class(token.Value);

            // fields
            if(_tokens.TryConsume(CDTokenType.Pipe))
            {
                // TODO there can be 0 fields! "[a||method()]"
                IField field = null;
                do
                {
                    field = parseField();
                    if(field != null)
                        c.Add(field); // TODO c == null?!

                    // remove "," if present
                    if(!_tokens.TryConsume(CDTokenType.Comma))
                        break;

                } while (field != null);

            }

            if (!_tokens.TryConsume(CDTokenType.Bracket_Close))
            {
                // TODO error
                return null;
            }

            return c;
        }

        // ID [ ":" ID ]
        public Model.IField parseField()
        {
            String name = null, type = null;
            CDToken token = null;

            // TODO no field? "[classname|]"
            if(_tokens.TryConsume(CDTokenType.Identifier, out token))
                name = token.Value;

            if (_tokens.TryConsume(CDTokenType.Colon))
            {
                if (_tokens.TryConsume(CDTokenType.Identifier, out token))
                    type = token.Value;
                else
                {
                    // TODO error!
                }
            }

            return name != null ? new Field(name, type) : null;
        }

        #endregion

        // class [ assoc class ]
        private bool parseClassOrAssociation(ClassDiagram cd)
        {
            // class
            var c = parseClass();
            if (c == null)
            {
                // error must have been flagged by class parsing
                return false;
            }
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
        private AssocInfo parseAssociation()
        {
            var sourceMult = parseMultiplicity();
            AssocInfo assocInfo = null;

            // TODO checks!!!
            if(_tokens.TryConsume(CDTokenType.Dash))
            {
                if(_tokens.TryConsume(CDTokenType.Angle_Close))
                    assocInfo = new AssocInfo(AssociationKind.Directed);
                else
                    assocInfo = new AssocInfo(AssociationKind.Undirected);
            }
            if(_tokens.TryConsume(CDTokenType.Angle_Open, CDTokenType.Angle_Close, CDTokenType.Dash, CDTokenType.Angle_Close))
            {
                assocInfo = new AssocInfo(AssociationKind.Aggregation);
            }
            if (_tokens.TryConsume(CDTokenType.Plus))
            {
                if(_tokens.TryConsume(CDTokenType.Dash, CDTokenType.Angle_Close))
                {
                    assocInfo = new AssocInfo(AssociationKind.Aggregation);
                }
                if(_tokens.TryConsume(CDTokenType.Plus, CDTokenType.Dash, CDTokenType.Angle_Close))
                {
                    assocInfo = new AssocInfo(AssociationKind.Composition);
                }
            }

            var targetMult = parseMultiplicity();

            if (assocInfo != null)
            {
                assocInfo.SourceMult = sourceMult;
                assocInfo.TargetMult = targetMult;
            }

            return assocInfo;
        }

        // TODO make public, provide specific tests.
        private Multiplicity parseMultiplicity()
        {
            Multiplicity m = null;
            
            CDToken token1, token2;
            if (_tokens.TryConsume(CDTokenType.Number, out token1))
            {
                if (_tokens.TryConsume(CDTokenType.DotDot))
                {
                    if (_tokens.TryConsume(CDTokenType.Number, out token2))
                    {
                        m = new Multiplicity(MultiplicityKind.SingleNumber, token1.Value, MultiplicityKind.SingleNumber, token2.Value);
                    }
                    else if (_tokens.TryConsume(CDTokenType.Star))
                    {
                        m = new Multiplicity(MultiplicityKind.SingleNumber, token1.Value, MultiplicityKind.Star, null);
                    }
                    else
                    {
                        // TODO ??
                    }
                }
                else
                {
                    m = new Multiplicity(MultiplicityKind.SingleNumber, token1.Value, MultiplicityKind.None, null);
                }
            }
            else
            {
                CDToken token;
                if (_tokens.TryConsume(CDTokenType.Star, out token))
                    m = new Multiplicity(MultiplicityKind.Star, token.Value, MultiplicityKind.None, null);
            }

            return m;
        }

        private readonly TokenStream _tokens;

    }

}
