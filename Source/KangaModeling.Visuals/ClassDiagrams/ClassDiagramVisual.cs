using System;
using System.Linq;
using KangaModeling.Compiler.ClassDiagrams.Model;
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

        public ClassDiagramVisual(IClassDiagram cd)
        {
            if (cd == null) throw new ArgumentNullException("cd");
            if (cd.Classes.Count() != 1) throw new ArgumentException("currently, only single class allowed");
            _mCd = cd;
            Initialize();
        }

        private void Initialize()
        {
            foreach (var @class in _mCd.Classes)
                AddChild(new ClassVisual(@class));
        }

        protected override void LayoutCore(Graphics.IGraphicContext graphicContext)
        {
            // 1. layout all classes. Now all classes have their Size set.
            base.LayoutCore(graphicContext);

            // 2. arrange classes somehow (this is the VERY hard part)
            var children = Children.ToList();

            Location = new Point(0f, 0f);
            Size = new Size(children[0].Size.Width + 20f, children[0].Size.Height + 20f);

            // need to set the Location of each child here!
            children[0].Location = new Point(10f, 10f);
        }
    }
}