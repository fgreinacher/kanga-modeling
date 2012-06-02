using System.Collections.Generic;

namespace KangaModeling.Compiler.ClassDiagrams.Model
{
    /// <summary>
    /// Represents a class in the CD model.
    /// </summary>
    public interface IClass : IDisplayable
    {
        /// <summary>
        /// The name of the class.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The fields contained in this class.
        /// </summary>
        IEnumerable<IField> Fields { get; }

        /// <summary>
        /// The methods contained in this class.
        /// </summary>
        IEnumerable<IMethod> Methods { get; }

    }

}
