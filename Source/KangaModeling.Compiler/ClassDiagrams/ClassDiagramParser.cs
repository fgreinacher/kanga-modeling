using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using KangaModeling.Compiler.ClassDiagrams.Errors;
using KangaModeling.Compiler.ClassDiagrams.Model;

namespace KangaModeling.Compiler.ClassDiagrams
{
    /// <summary>
    /// LL(k) recursive descent parser for class diagram strings.
    /// </summary>
    class ClassDiagramParser
    {

        private readonly ClassDiagramTokenStream _genericTokens;
        private readonly ErrorCallback _errorCallback;

        #region types used for error handling

        public delegate ErrorReturnCode ErrorCallback(SyntaxErrorType type, TokenType expected, ClassDiagramToken actualToken);

        #endregion

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

            public void AddClass(IClass clazz) {
                if (clazz == null) throw new ArgumentNullException("clazz");
                _classes.Add(clazz);
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
                if (name == null) throw new ArgumentNullException("name");
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
                    sb.Append(string.Format("{0}", Name));
                    if (Type != null)
                        sb.Append(string.Format(": {0}", Type));
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

                TargetRole = string.Empty;
                SourceRole = string.Empty;
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

                    methodNameBuilder.Append(")");

                    if (null != ReturnType)
                        methodNameBuilder.Append(string.Format(" : {0}", ReturnType));

                    return methodNameBuilder.ToString();
                }
            }
        }

        #endregion

        public ClassDiagramParser(ClassDiagramTokenStream genericTokens, ErrorCallback errorCallback = null)
        {
            if (genericTokens == null) throw new ArgumentNullException("genericTokens");
            _genericTokens = genericTokens;
            _errorCallback = errorCallback;
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
                FireError(SyntaxErrorType.Unexpected);
                return null;
            }

            return cd;
        }

        // TODO there's no TokenType for junk...
        private void FireError(SyntaxErrorType syntaxErrorType, TokenType expected = TokenType.Unknown)
        {
            if(_errorCallback != null)
                _errorCallback(syntaxErrorType, expected, _genericTokens.Count > 0 ? _genericTokens[0] : null);
        }

        // "[" ID "]"
        private IClass ParseClass()
        {
            if (!_genericTokens.TryConsume(TokenType.BracketOpen))
            {
                FireError(SyntaxErrorType.Missing, TokenType.BracketOpen);
                return null;
            }

            ClassDiagramToken token;
            if (!_genericTokens.TryConsume(TokenType.Identifier, out token))
            {
                FireError(SyntaxErrorType.Missing, TokenType.Identifier);
                return null;
            }
            var c = new Class(token.Value);

            // fields
            if(_genericTokens.TryConsume(TokenType.Pipe))
            {
                IField field;
                do
                {
                    field = ParseField();
                    if(field != null)
                        c.Add(field);

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
                        c.Add(m);

                    // remove "," if present
                    if (!_genericTokens.TryConsume(TokenType.Comma))
                        break;

                } while (m != null);

            }

            if (!_genericTokens.TryConsume(TokenType.BracketClose))
            {
                FireError(SyntaxErrorType.Missing, TokenType.BracketClose);
                return null;
            }

            return c;
        }

        // ID [ ":" ID ]
        private IField ParseField()
        {
            String name, type = null;
            ClassDiagramToken token;
            VisibilityModifier vm;

            // handle visibility modifiers (OPTIONAL)
            TryConsumeVisibilityModifier(out vm);

            if (_genericTokens.TryConsume(TokenType.Identifier, out token))
            {
                name = token.Value;
            }
            else
            {
                // TODO FireError(TokenType.Identifier);
                return null;
            }

            if (_genericTokens.TryConsume(TokenType.Colon))
            {
                if (_genericTokens.TryConsume(TokenType.Identifier, out token))
                    type = token.Value;
                else
                {
                    FireError(SyntaxErrorType.Missing, TokenType.Identifier);
                    return null;
                }
            }

            return name != null ? new Field(name, type, vm) : null;
        }

        private IMethod ParseMethod()
        {
            VisibilityModifier vis;
            string rettype = "void";
            ClassDiagramToken token;

            // (optional) visibility
            TryConsumeVisibilityModifier(out vis);

            // (mandatory) name
            if(!_genericTokens.TryConsume(TokenType.Identifier, out token))
            {
                FireError(SyntaxErrorType.Missing, TokenType.Identifier);
                return null;
            }
            string name = token.Value;

            // (mandatory) (
            if(!_genericTokens.TryConsume(TokenType.ParenthesisOpen))
            {
                FireError(SyntaxErrorType.Missing, TokenType.ParenthesisOpen);
                return null;
            }

            // method parameters
            var mp = new List<MethodParameter>();
            do
            {
                // TODO should be done using lookahead...
                if (!_genericTokens.TryConsume(TokenType.Identifier, out token))
                    break; // no identifier -> finished
                var paramType = token.Value;
                string paramName;

                if (_genericTokens.TryConsume(TokenType.Identifier, out token))
                {
                    paramName = token.Value;
                }
                else
                {
                    // no second identifier -> first one actually is the name, and there's no type.
                    paramName = paramType;
                    paramType = string.Empty;
                }

                mp.Add(new MethodParameter(paramName, paramType));

                if (!_genericTokens.TryConsume(TokenType.Comma))
                    break;

            } while (true);

            // (mandatory) )
            if (!_genericTokens.TryConsume(TokenType.ParenthesisClose))
            {
                FireError(SyntaxErrorType.Missing, TokenType.ParenthesisClose);
                return null;
            }

            // (optional) return type
            if(_genericTokens.TryConsume(TokenType.Colon))
            {
                // now return type MUST follow
                if(!_genericTokens.TryConsume(TokenType.Identifier, out token))
                {
                    FireError(SyntaxErrorType.Missing, TokenType.Identifier);
                    return null;
                }

                rettype = token.Value;
            }

            return new Method(name, rettype, mp, vis);
        }

        #endregion

        private void TryConsumeVisibilityModifier(out VisibilityModifier mod)
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
                var c2 = ParseClass();
                if(c2 == null)
                {
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
            public readonly AssociationKind Kind;

            public Multiplicity SourceMult;
            public Multiplicity TargetMult;
        }

        // simple ones: "->" "<>->" "+->" "++->"
        private AssocInfo ParseAssociation()
        {
            var sourceMult = ParseMultiplicity();
            AssocInfo assocInfo = null;

            if(_genericTokens.TryConsume(TokenType.Dash))
            {
                assocInfo = new AssocInfo(_genericTokens.TryConsume(TokenType.AngleClose) ? AssociationKind.Directed : AssociationKind.Undirected);
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
                    else
                        // TODO more than one TokenType!
                        FireError(SyntaxErrorType.Missing, TokenType.Number);
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

    }

}
