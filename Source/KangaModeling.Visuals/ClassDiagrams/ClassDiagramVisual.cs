using System;
using System.Collections.Generic;
using System.Linq;
using KangaModeling.Compiler.ClassDiagrams;
using KangaModeling.Compiler.ClassDiagrams.Model;
using KangaModeling.Compiler.Toolbox;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.ClassDiagrams
{
    /// <summary>
    /// A ClassDiagramVisual is able to visualize IClassDiagram instances.
    /// </summary>
    public sealed class ClassDiagramVisual : Visual
    {
        /// <summary> the diagram to visualize. </summary>
        private readonly IClassDiagram _mCd;

        private readonly Dictionary<IClass, ClassVisual> _classVisuals;
        private readonly Dictionary<IAssociation, AssociationVisual> _associationVisuals;

        public ClassDiagramVisual(IClassDiagram cd)
        {
            if (cd == null) throw new ArgumentNullException("cd");
            _mCd = cd;

            _classVisuals = new Dictionary<IClass, ClassVisual>();
            _associationVisuals = new Dictionary<IAssociation, AssociationVisual>();

            Initialize();
        }

        private void Initialize()
        {
            foreach (var @class in _mCd.Classes)
            {
                var cv = new ClassVisual(@class);
                _classVisuals[@class] = cv;
                AddChild(cv);
            }

            foreach(var assoc in _mCd.Associations)
            {
                var assocVisual = new AssociationVisual(assoc, this);
                _associationVisuals[assoc] = assocVisual;
                AddChild(assocVisual);
            }
        }

        public ClassVisual GetClassVisual(IClass iClass)
        {
            if (iClass == null) throw new ArgumentNullException("iClass");
            return _classVisuals[iClass];
        }

        protected override void LayoutCore(Graphics.IGraphicContext graphicContext)
        {
            // 1. layout all classes. Now all classes have their Size set.
            base.LayoutCore(graphicContext);

            // 2. arrange classes somehow (this is the VERY hard part)
            float xCursor = 0f;
            foreach(var classVisual in _classVisuals.Values)
            {
                classVisual.Location = new Point(xCursor, 0f);
                xCursor += classVisual.Size.Width + 100f;
            }

            // need to re-layout the associations.
            // layouting has done for all children before, but associations
            // need the Location be set for the classes, and these have been set just now.
            _associationVisuals.ForEach(kvp => kvp.Value.Layout(graphicContext));

            Location = new Point(0f, 0f);
            Size = new Size(xCursor, 100f);
        }
    }
}