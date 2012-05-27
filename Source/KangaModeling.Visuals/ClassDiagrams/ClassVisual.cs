using System;
using System.Collections.Generic;
using System.Linq;
using KangaModeling.Compiler.ClassDiagrams;
using KangaModeling.Compiler.ClassDiagrams.Model;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.ClassDiagrams
{

    /// <summary>
    /// A ClassVisual instance is able to visualize an instance of IClass.
    /// </summary>
    public sealed class ClassVisual : Visual
    {
        /// <summary>The IClass instance to visualize</summary>
        private readonly IClass _class;

        public ClassVisual(IClass cd)
        {
            if (cd == null) throw new ArgumentNullException("cd");
            _class = cd;

            AddChild(new CompartmentVisual(Enumerable.Repeat(_class, 1)));
            AddChild(new CompartmentVisual(_class.Fields));
            AddChild(new CompartmentVisual(_class.Methods));
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            // TODO debug flag graphicContext.FillRectangle(Location, Size, Color.Green);
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            SetCompartmentLocations();

            // the width of individual compartments may not match.
            // get the widest one and set the widths of all compartments.
            var maxWidth = Children.Max(v => v.Width);
            Children.ForEach(v => v.Size = new Size(maxWidth, v.Size.Height));

            Size = new Size(maxWidth, Children.Aggregate(0f, (f, visual) => f + visual.Height));
        }

        private void SetCompartmentLocations()
        {
            float yCursor = 0f;
            foreach (var compartments in Children)
            {
                compartments.Location = new Point(0f, yCursor);
                yCursor += compartments.Size.Height;
            }
        }

    }
}