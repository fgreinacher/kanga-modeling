using KangaModeling.Compiler.ClassDiagrams.Model;
using KangaModeling.Graphics.Primitives;
using System;
using System.Linq;

namespace KangaModeling.Visuals.ClassDiagrams
{
    public sealed class ClassDiagramVisual : Visual
    {
        private readonly IClassDiagram m_CD;

        // TODO Styles are not bound to Sequence Diagrams!
        public ClassDiagramVisual(IClassDiagram cd)
            : base()
        {
            if (cd == null) throw new ArgumentNullException("cd");
            if (cd.Classes.Count() != 1) throw new ArgumentException("currently, only single class allowed");
            m_CD = cd;
            Initialize();
        }

        private void Initialize()
        {
            foreach (var @class in m_CD.Classes)
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