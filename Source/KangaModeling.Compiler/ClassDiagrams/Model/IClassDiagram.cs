using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Compiler.ClassDiagrams.Model
{

    /// <summary>
    /// Represents the complete CD.
    /// </summary>
    interface IClassDiagram
    {
        
        IEnumerable<IClass> Classes { get; }

        IEnumerable<IAssociation> Associations { get; }
    
    }

}
