using System.Collections.Generic;
using System.Text;
using KangaModeling.Compiler.ClassDiagrams.Model;
using KangaModeling.Graphics.Primitives;
using System;

namespace KangaModeling.Visuals.ClassDiagrams
{
    public sealed class ClassVisual : Visual
    {
        private readonly IClass _class;

        private Size _classNameTextSize;
        private Point _classNameTextPoint;
        
        private CompartmentVisualInfo _classnameCompartment;
        private CompartmentVisualInfo _fieldsCompartment;

        public ClassVisual(IClass cd)
        {
            if (cd == null) throw new ArgumentNullException("cd");
            _class = cd;
            _fieldInfos = new Dictionary<IField, FieldVisualInfo>();
        }

        protected override void DrawCore(Graphics.IGraphicContext graphicContext)
        {
            graphicContext.DrawRectangle(_classnameCompartment.Location, _classnameCompartment.Size, Color.Black, LineStyle.Sketchy);
            graphicContext.DrawRectangle(_fieldsCompartment.Location, _fieldsCompartment.Size, Color.Black, LineStyle.Sketchy);

            // draw the class name
            graphicContext.DrawText(
                _classNameTextPoint,
                _classNameTextSize,
                _class.Name,
                Font.Handwritten,
                12,
                Color.Black,
                HorizontalAlignment.Center,
                VerticalAlignment.Middle
            );

            // draw the fields
            foreach(var fi in _fieldInfos)
            {
                graphicContext.DrawText(
                    fi.Value.TextPosition,
                    fi.Value.TextSize,
                    fi.Value.FieldText,
                    Font.Handwritten,
                    12,
                    Color.Black,
                    HorizontalAlignment.Center,
                    VerticalAlignment.Middle
                );
            }
        }

        // TODO only class name is used for the Width, field names are NOT considered.
        protected override void LayoutCore(Graphics.IGraphicContext graphicContext)
        {
            // layout the class name compartment
            var textSize = graphicContext.MeasureText(_class.Name, Font.Handwritten, 12);

            // some padding left and right...
            const float leftPadding = 10f;
            const float rightPadding = 10f;

            _classNameTextSize = textSize;
            _classNameTextPoint = new Point(leftPadding, 4f);

            _classnameCompartment = new CompartmentVisualInfo {
                Location = new Point(0f, 0f),
                Size = new Size (
                    leftPadding + textSize.Width + rightPadding,
                    4f + textSize.Height + 4f
                )
            };

            // layout the fields of the class
            float yCursor = _classnameCompartment.Size.Height;
            float maxFieldWidth = _classnameCompartment.Size.Width;
            foreach(var field in _class.Fields) {
                var text = GenerateFieldText(field);
                var fieldTextSize = graphicContext.MeasureText(text, Font.Handwritten, 12);
                _fieldInfos[field] = new FieldVisualInfo {
                    TextSize = fieldTextSize,
                    FieldText = text,
                    TextPosition = new Point(10f, yCursor)
                };
                yCursor += fieldTextSize.Height;

                maxFieldWidth = Math.Max(maxFieldWidth, fieldTextSize.Width + 10f);
            }

            // TODO extension method on Size?
            _classnameCompartment.Size = new Size(maxFieldWidth, _classnameCompartment.Size.Height);
            _fieldsCompartment = new CompartmentVisualInfo {
                Location = new Point(0f, _classnameCompartment.Size.Height),
                Size = new Size(maxFieldWidth, yCursor - _classnameCompartment.Size.Height)
            };

            Size = new Size(maxFieldWidth, yCursor);
        }

        private String GenerateFieldText(IField field)
        {
            if (field == null) throw new ArgumentNullException("field");
            var sb = new StringBuilder();

            switch(field.Visibility)
            {
                case VisibilityModifier.Public: sb.Append("+");break; // TODO mapping from enum to string is multiple defined!
                case VisibilityModifier.Protected: sb.Append("#"); break;
                case VisibilityModifier.Private: sb.Append("~"); break;
                case VisibilityModifier.Internal: sb.Append("-"); break;
            }

            sb.Append(string.Format("{0}", field.Name));
            if(field.Type != null)
                sb.Append(string.Format(": {0}", field.Type));

            return sb.ToString();
        }

        private struct FieldVisualInfo
        {
            public Size TextSize;
            public String FieldText;
            public Point TextPosition;
        }

        private struct CompartmentVisualInfo
        {
            public Size Size;
            public Point Location;
        }

        private readonly Dictionary<IField, FieldVisualInfo> _fieldInfos;

    }

}