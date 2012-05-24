using System.Collections.Generic;

namespace KangaModeling.Compiler.ClassDiagrams.Model
{

    /// <summary>
    /// The visilibity of a member
    /// </summary>
    public enum VisibilityModifier
    {
        Public,
        Internal,
        Private,
        Protected
    }

    /// <summary>
    /// Represents one field of an IClass instance.
    /// </summary>
    public interface IField
    {
        string Name { get; }
        string Type { get; }
        VisibilityModifier Visibility { get; }
    }

    public interface IMethod
    {
        string Name { get; }
        string ReturnType { get; }
        IEnumerable<MethodParameter> Parameters { get; }
        VisibilityModifier Visibility { get; }
    }

    public struct MethodParameter
    {
        public MethodParameter(string name, string type) : this()
        {
            Name = name;
            Type = type;
        }

        public string Name { get; private set; }
        public string Type { get; private set; }
    }

    /// <summary>
    /// Represents a class in the CD model.
    /// </summary>
    public interface IClass
    {
        /// <summary>
        /// The name of the class.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The fields contained in this class.
        /// </summary>
        IEnumerable<IField> Fields { get; }

    }

}
