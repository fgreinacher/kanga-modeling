using System.Collections.Generic;

namespace KangaModeling.Compiler.ClassDiagrams.Model
{
    public interface IMethod
    {
        string Name { get; }
        string ReturnType { get; }
        IEnumerable<MethodParameter> Parameters { get; }
        VisibilityModifier Visibility { get; }
    }

    public struct MethodParameter
    {
        public MethodParameter(string name, string type)
            : this()
        {
            Name = name;
            Type = type;
        }

        public string Name { get; private set; }
        public string Type { get; private set; }
    }
}