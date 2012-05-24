using System.Collections.Generic;

namespace KangaModeling.Compiler.ClassDiagrams.Model
{

    /// <summary>
    /// Represents the complete CD.
    /// </summary>
    public interface IClassDiagram
    {
        
        /// <summary>
        /// The classes contained in the diagram.
        /// </summary>
        IEnumerable<IClass> Classes { get; }

        /// <summary>
        /// The associations between classes in the diagram.
        /// </summary>
        IEnumerable<IAssociation> Associations { get; }
    
    }

}
