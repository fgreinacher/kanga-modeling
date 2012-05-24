using System;
using System.Linq;
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
            _methodInfos = new Dictionary<IMethod, FieldVisualInfo>();
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            _classnameCompartment.DrawCore(graphicContext);
            _classnameInfo.DrawCore(graphicContext);

            _fieldsCompartment.DrawCore(graphicContext);
            foreach(var fi in _fieldInfos)
                fi.Value.DrawCore(graphicContext);

            if (_methodInfos.Count > 0)
                _methodsCompartment.DrawCore(graphicContext);
            foreach (var mi in _methodInfos)
                mi.Value.DrawCore(graphicContext);
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            LayoutClassnameCompartment(graphicContext);
            LayoutFieldsCompartment(graphicContext);
            LayoutMethodsCompartment(graphicContext);

            var totalYSize = _classnameCompartment.Size.Height + _fieldsCompartment.Size.Height + _methodsCompartment.Size.Height;
            var maxFieldWidth = Math.Max(_classnameCompartment.Size.Width, _fieldsCompartment.Size.Width);
            maxFieldWidth = Math.Max(maxFieldWidth, _methodsCompartment.Size.Width);

            // need to set the width on each compartment...
            _classnameCompartment.Size = new Size(maxFieldWidth, _classnameCompartment.Size.Height);
            _fieldsCompartment.Size = new Size(maxFieldWidth, _fieldsCompartment.Size.Height);
            _methodsCompartment.Size = new Size(maxFieldWidth, _methodsCompartment.Size.Height);

            Size = new Size(maxFieldWidth, totalYSize);
        }

        private void LayoutFieldsCompartment(IGraphicContext graphicContext)
        {
            const float topBottomPadding = 5f;
            float yCursor = _classnameCompartment.Size.Height + topBottomPadding;
            float maxFieldWidth = 0f;
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
            yCursor += topBottomPadding;

            // TODO extension method on Size?
            // TODO setting the classname compartment size HAS NOTHING VERLOREN HIER!
            _classnameCompartment.Size = new Size(maxFieldWidth, _classnameCompartment.Size.Height);
            _fieldsCompartment = new CompartmentVisualInfo {
                Location = new Point(0f, _classnameCompartment.Size.Height),
                Size = new Size(maxFieldWidth, yCursor - _classnameCompartment.Size.Height)
            };
        }

        private void LayoutMethodsCompartment(IGraphicContext graphicContext)
        {
            const float topBottomPadding = 5f;
            float yCursor = _fieldsCompartment.Size.Height + _fieldsCompartment.Location.Y + topBottomPadding;
            float maxFieldWidth = 0f;
            foreach (var method in _class.Methods)
            {
                var text = GenerateMethodText(method);
                var fieldTextSize = graphicContext.MeasureText(text, Font.Handwritten, 12);
                _methodInfos[method] = new FieldVisualInfo
                {
                    TextSize = fieldTextSize,
                    FieldText = text,
                    TextPosition = new Point(10f, yCursor)
                };
                yCursor += fieldTextSize.Height;

                maxFieldWidth = Math.Max(maxFieldWidth, fieldTextSize.Width + 10f);
            }
            yCursor += topBottomPadding;

            _methodsCompartment = new CompartmentVisualInfo
            {
                Location = new Point(0f, _fieldsCompartment.Size.Height + _fieldsCompartment.Location.Y),
                Size = new Size(maxFieldWidth, yCursor - _fieldsCompartment.Size.Height - _fieldsCompartment.Location.Y)
            };
        }

        private string GenerateMethodText(IMethod method)
        {
            var methodNameBuilder = new StringBuilder();

            // TODO ToString() on IMethod!
            methodNameBuilder.Append(Visibility2String(method.Visibility));
            methodNameBuilder.Append(method.Name);
            methodNameBuilder.Append("(");
            foreach(var parameter in method.Parameters)
            {
                methodNameBuilder.Append(parameter.Type + " " + parameter.Name);
                methodNameBuilder.Append(", ");
            }
            if(method.Parameters.Any())
                methodNameBuilder.Remove(methodNameBuilder.Length - 2, 2);

            // TODO return type

            methodNameBuilder.Append(")");

            return methodNameBuilder.ToString();
        }

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

        // TODO ToString on IField!
        private static String GenerateFieldText(IField field)
        {
            if (field == null) throw new ArgumentNullException("field");
            var sb = new StringBuilder();

            sb.Append(Visibility2String(field.Visibility));

            sb.Append(string.Format("{0}", field.Name));
            if(field.Type != null)
                sb.Append(string.Format(": {0}", field.Type));

            return sb.ToString();
        }

        private static string Visibility2String(VisibilityModifier visibilityModifier)
        {
            switch (visibilityModifier)
            {
                case VisibilityModifier.Public:
                    return "+";
                case VisibilityModifier.Protected:
                    return "#";
                case VisibilityModifier.Private:
                    return "~";
                case VisibilityModifier.Internal:
                    return "-";
            }

            throw new ArgumentException("unexpected visibility modifier: " + visibilityModifier.ToString());
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
        private readonly Dictionary<IMethod, FieldVisualInfo> _methodInfos;

    }

}