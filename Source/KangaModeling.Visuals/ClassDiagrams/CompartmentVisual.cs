using System;
using System.Collections.Generic;
using System.Linq;
using KangaModeling.Compiler.ClassDiagrams.Model;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.ClassDiagrams
{
    public sealed class CompartmentVisual : Visual
    {
        private readonly IEnumerable<IDisplayable> _members;

        public CompartmentVisual(IEnumerable<IDisplayable> members)
        {
            if (members == null) throw new ArgumentNullException("members");
            _members = members;

            foreach (var displayable in _members)
                AddChild(new PaddedTextVisual(displayable));
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            // the padding around ALL members
            //var defaultPadding = new Padding(0f, 0f, 0f, 0f);

            float yCursor = 0f;

            float maxFieldWidth = 0f;
            foreach (var child in Children)
            {
                child.Location = new Point(0f, yCursor);
                yCursor += child.Size.Height;
                maxFieldWidth = Math.Max(maxFieldWidth, child.Size.Width);
            }
            //yCursor += defaultPadding.Bottom;

            Size = new Size(maxFieldWidth, yCursor);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            if (_members.Count() != 0)
                graphicContext.DrawRectangle(Point.Empty, Size, Color.Black, LineStyle.Sketchy);
        }

    }
}