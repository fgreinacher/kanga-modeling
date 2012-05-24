using System;
using System.Collections.Generic;
using System.Text;
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

        private ClassNameVisualInfo _classnameInfo;
        private CompartmentVisualInfo _classnameCompartment;
        private CompartmentVisualInfo _fieldsCompartment;
        private CompartmentVisualInfo _methodsCompartment;

        public ClassVisual(IClass cd)
        {
            if (cd == null) throw new ArgumentNullException("cd");
            _class = cd;
            _fieldInfos = new Dictionary<IField, FieldVisualInfo>();
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            _classnameCompartment.DrawCore(graphicContext);
            _classnameInfo.DrawCore(graphicContext);

            _fieldsCompartment.DrawCore(graphicContext);
            foreach(var fi in _fieldInfos)
                fi.Value.DrawCore(graphicContext);
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            LayoutClassnameCompartment(graphicContext);
            LayoutFieldsCompartment(graphicContext);

            var totalYSize = _classnameCompartment.Size.Height + _fieldsCompartment.Size.Height;
            var maxFieldWidth = Math.Max(_classnameCompartment.Size.Width, _fieldsCompartment.Size.Width);

            Size = new Size(maxFieldWidth, totalYSize);
        }

        private void LayoutFieldsCompartment(IGraphicContext graphicContext)
        {
            float yCursor = _classnameCompartment.Size.Height;
            float maxFieldWidth = _classnameCompartment.Size.Width;
            foreach (var field in _class.Fields)
            {
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
        }

        //private void LayoutMethodsCompartment(IGraphicContext graphicContext)
        //{
        //    float yCursor = _methodsCompartment.Size.Height;
        //    float maxFieldWidth = _methodsCompartment.Size.Width;
        //    foreach (var method in _class.)
        //    {
        //        var text = GenerateFieldText(field);
        //        var fieldTextSize = graphicContext.MeasureText(text, Font.Handwritten, 12);
        //        _fieldInfos[field] = new FieldVisualInfo
        //        {
        //            TextSize = fieldTextSize,
        //            FieldText = text,
        //            TextPosition = new Point(10f, yCursor)
        //        };
        //        yCursor += fieldTextSize.Height;

        //        maxFieldWidth = Math.Max(maxFieldWidth, fieldTextSize.Width + 10f);
        //    }

        //    // TODO extension method on Size?
        //    _classnameCompartment.Size = new Size(maxFieldWidth, _classnameCompartment.Size.Height);
        //    _fieldsCompartment = new CompartmentVisualInfo
        //    {
        //        Location = new Point(0f, _classnameCompartment.Size.Height),
        //        Size = new Size(maxFieldWidth, yCursor - _classnameCompartment.Size.Height)
        //    };
        //}

        private void LayoutClassnameCompartment(IGraphicContext graphicContext)
        {
            var textSize = graphicContext.MeasureText(_class.Name, Font.Handwritten, 12);

            // some padding left and right...
            const float leftPadding = 10f;
            const float rightPadding = 10f;

            _classnameInfo = new ClassNameVisualInfo {
                Text = _class.Name,
                TextSize = textSize,
                Location = new Point(leftPadding, 4f)
            };

            _classnameCompartment = new CompartmentVisualInfo {
                Location = new Point(0f, 0f),
                Size = new Size(
                    leftPadding + textSize.Width + rightPadding,
                    4f + textSize.Height + 4f
                )
            };
        }

        private static String GenerateFieldText(IField field)
        {
            if (field == null) throw new ArgumentNullException("field");
            var sb = new StringBuilder();

            // TODO mapping from enum to string is multiple defined!
            switch(field.Visibility)
            {
                case VisibilityModifier.Public: sb.Append("+");break;
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
            public void DrawCore(IGraphicContext graphicContext)
            {
                graphicContext.DrawText(
                    TextPosition,
                    TextSize,
                    FieldText,
                    Font.Handwritten,
                    12,
                    Color.Black,
                    HorizontalAlignment.Center,
                    VerticalAlignment.Middle
                );
            }

            public Size TextSize;
            public String FieldText;
            public Point TextPosition;
        }

        private struct CompartmentVisualInfo
        {
            public void DrawCore(IGraphicContext graphicContext)
            {
                graphicContext.DrawRectangle(Location, Size, Color.Black, LineStyle.Sketchy);
            }

            public Size Size;
            public Point Location;
        }

        private struct ClassNameVisualInfo
        {
            public void DrawCore(IGraphicContext graphicContext)
            {
                graphicContext.DrawText(
                    Location,
                    TextSize,
                    Text,
                    Font.Handwritten,
                    12,
                    Color.Black,
                    HorizontalAlignment.Center,
                    VerticalAlignment.Middle
                );
            }

            public String Text;
            public Size TextSize;
            public Point Location;
        }

        private readonly Dictionary<IField, FieldVisualInfo> _fieldInfos;

    }

}