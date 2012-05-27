using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            return new ClassDiagramParser(new ClassDiagramScanner().Parse(text)).ParseClassDiagram();
        }

    }
    
    /// <summary>
    /// LL(k) recursive descent parser for class diagram strings.
    /// </summary>
    class ClassDiagramParser
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

        [DebuggerDisplay("Class {Name}")]
        private class Class : IClass
        {
            private readonly List<IField> _fields;
            private readonly List<IMethod> _methods;
            public Class(string name)
            {
                // TODO null check?
                Name = name;
                _fields = new List<IField>(2);
                _methods = new List<IMethod>(2);
            }

            public void Add(IField field)
            {
                if (field == null) throw new ArgumentNullException("field");
                _fields.Add(field);
            }

            public void Add(IMethod method)
            {
                if (method == null) throw new ArgumentNullException("method");
                _methods.Add(method);
            }

            public IEnumerable<IField> Fields
            {
                get { return _fields; }
            }

            public IEnumerable<IMethod> Methods
            {
                get { return _methods; }
            }

            public string DisplayText
            {
                get { return Name; }
            }

            public string Name { get; private set; }
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

            public string DisplayText
            {
                get
                {
                    var sb = new StringBuilder();
                    sb.Append(Visibility.GetDisplayText());
                    sb.Append(string.Format("{0}", this.Name));
                    if (Type != null)
                        sb.Append(string.Format(": {0}", this.Type));
                    return sb.ToString();
                }
            }
        }

        [DebuggerDisplay("Association Source:{Source.Name} Target:{Target.Name}")]
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

            public string DisplayText
            {
                get {
                    var methodNameBuilder = new StringBuilder();

                    methodNameBuilder.Append(Visibility.GetDisplayText());
                    methodNameBuilder.Append(Name);
                    methodNameBuilder.Append("(");
                    foreach (var parameter in Parameters)
                    {
                        methodNameBuilder.Append(parameter.Type + " " + parameter.Name);
                        methodNameBuilder.Append(", ");
                    }
                    if (Parameters.Any())
                        methodNameBuilder.Remove(methodNameBuilder.Length - 2, 2);

                    // TODO return type

                    methodNameBuilder.Append(")");

                    return methodNameBuilder.ToString();
                }
            }
        }

        #endregion

        public ClassDiagramParser(ClassDiagramTokenStream genericTokens)
        {
            if (genericTokens == null) throw new ArgumentNullException("genericTokens");
            _genericTokens = genericTokens;
        }

        #region productions

        // assoc [ "," assoc ]* 
        public IClassDiagram ParseClassDiagram()
        {
            var cd = new ClassDiagram();

            while (_genericTokens.Count > 0)
            {
                if (!ParseClassOrAssociation(cd))
                    return null;

                // either no more genericTokens OR comma, otherwise error
                if (!_genericTokens.TryConsume(TokenType.Comma))
                    break;
            }

            if (_genericTokens.Count > 0)
            {
                // TODO ERROR - junk at end
                return null;
            }

            return cd;
        }

        // "[" ID "]"
        public IClass ParseClass()
        {
            if (!_genericTokens.TryConsume(TokenType.BracketOpen))
            {
                // TODO error
                return null;
            }

            // TODO must be identifier!
            // TODO what if there is no token?
            //if (_genericTokens[0].TokenType != TokenType.Identifier)
            //{
            //    // error: expected identifier.
            //    return null;
            //}
            ClassDiagramToken token;
            if (!_genericTokens.TryConsume(TokenType.Identifier, out token))
            {
                // error
                return null;
            }
            var c = new Class(token.Value);

            // fields
            if(_genericTokens.TryConsume(TokenType.Pipe))
            {
                // TODO there can be 0 fields! "[a||method()]"
                IField field;
                do
                {
                    field = ParseField();
                    if(field != null)
                        c.Add(field); // TODO c == null?!

                    // remove "," if present
                    if(!_genericTokens.TryConsume(TokenType.Comma))
                        break;

                } while (field != null);

            }

            // methods
            if (_genericTokens.TryConsume(TokenType.Pipe))
            {
                IMethod m;
                do
                {
                    m = ParseMethod();
                    if (m != null)
                        c.Add(m); // TODO c == null?!

                    // remove "," if present
                    if (!_genericTokens.TryConsume(TokenType.Comma))
                        break;

                } while (m != null);

            }

            if (!_genericTokens.TryConsume(TokenType.BracketClose))
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
            ClassDiagramToken token;
            VisibilityModifier vm;

            // handle visibility modifiers (OPTIONAL)
            TryConsumeVisibilityModifier(out vm);

            // TODO no field? "[classname|]"
            if(_genericTokens.TryConsume(TokenType.Identifier, out token))
                name = token.Value;
            // TODO else error

            if (_genericTokens.TryConsume(TokenType.Colon))
            {
                if (_genericTokens.TryConsume(TokenType.Identifier, out token))
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
            ClassDiagramToken token;

            // (optional) visibility
            TryConsumeVisibilityModifier(out vis);

            // (mandatory) name
            if(!_genericTokens.TryConsume(TokenType.Identifier, out token))
            {
                // TODO ERROR
                return null;
            }
            name = token.Value;

            // (mandatory) (
            if(!_genericTokens.TryConsume(TokenType.ParenthesisOpen))
            {
                // TODO error
                return null;
            }

            // method parameters
            var mp = new List<MethodParameter>();
            do
            {
                if (!_genericTokens.TryConsume(TokenType.Identifier, out token))
                    break; // no identifier -> finished
                var paramType = token.Value;

                if (!_genericTokens.TryConsume(TokenType.Identifier, out token))
                {
                    // TODO error; after method parameter name, the type!
                    break;
                }

                mp.Add(new MethodParameter(token.Value, paramType));
            } while (true);

            // (mandatory) )
            if (!_genericTokens.TryConsume(TokenType.ParenthesisClose))
            {
                // TODO error
                return null;
            }

            // (optional) return type
            if(_genericTokens.TryConsume(TokenType.Colon))
            {
                // now return type MUST follow
                if(!_genericTokens.TryConsume(TokenType.Identifier, out token))
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
            if (_genericTokens.TryConsume(TokenType.Plus))
                mod = VisibilityModifier.Public;
            else if (_genericTokens.TryConsume(TokenType.Dash))
                mod = VisibilityModifier.Private;
            else if (_genericTokens.TryConsume(TokenType.Hash))
                mod = VisibilityModifier.Protected;
            else if (_genericTokens.TryConsume(TokenType.Tilde))
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
            var sourceClass = c;
            do
            {
                var assoc = ParseAssociation();
                if (assoc == null)
                    break;

                // assoc did parse, must be followed by class
                var c2 = ParseClass(); // TODO what if this returns null?
                if(c2 == null)
                {
                    // TODO parse error!
                    return false;
                }
                cd.AddClass(c2);

                cd.Add(new Association(assoc, sourceClass, c2));
                sourceClass = c2;
            } while (true);

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
            if(_genericTokens.TryConsume(TokenType.Dash))
            {
                if(_genericTokens.TryConsume(TokenType.AngleClose))
                    assocInfo = new AssocInfo(AssociationKind.Directed);
                else
                    assocInfo = new AssocInfo(AssociationKind.Undirected);
            }
            if(_genericTokens.TryConsume(TokenType.AngleOpen, TokenType.AngleClose, TokenType.Dash, TokenType.AngleClose))
            {
                assocInfo = new AssocInfo(AssociationKind.Aggregation);
            }
            if (_genericTokens.TryConsume(TokenType.Plus))
            {
                if(_genericTokens.TryConsume(TokenType.Dash, TokenType.AngleClose))
                {
                    assocInfo = new AssocInfo(AssociationKind.Aggregation);
                }
                if(_genericTokens.TryConsume(TokenType.Plus, TokenType.Dash, TokenType.AngleClose))
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

            ClassDiagramToken token1;
            if (_genericTokens.TryConsume(TokenType.Number, out token1))
            {
                if (_genericTokens.TryConsume(TokenType.DotDot))
                {
                    ClassDiagramToken token2;
                    if (_genericTokens.TryConsume(TokenType.Number, out token2))
                    {
                        m = new Multiplicity(MultiplicityKind.SingleNumber, token1.Value, MultiplicityKind.SingleNumber, token2.Value);
                    }
                    else if (_genericTokens.TryConsume(TokenType.Star))
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
                ClassDiagramToken token;
                if (_genericTokens.TryConsume(TokenType.Star, out token))
                    m = new Multiplicity(MultiplicityKind.Star, token.Value, MultiplicityKind.None, null);
            }

            return m;
        }

        private readonly ClassDiagramTokenStream _genericTokens;

    }

}
