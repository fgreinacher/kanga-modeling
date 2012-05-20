using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Compiler.ClassDiagrams.Model
{
    /// <summary>
    /// Represents a class in the CD model.
    /// </summary>
    interface IClass
    {
        /// <summary>
        /// The name of the class.
        /// </summary>
        string Name { get; }

    }

}
