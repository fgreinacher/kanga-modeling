using System.Collections.Generic;

namespace KangaModeling.Compiler.ClassDiagrams.Model
{
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
