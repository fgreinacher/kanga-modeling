using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Compiler.ClassDiagrams.Model
{

    /// <summary>
    /// Represents one field of an IClass instance.
    /// </summary>
    public interface IField
    {
        string Name { get; }
        string Type { get; }
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
