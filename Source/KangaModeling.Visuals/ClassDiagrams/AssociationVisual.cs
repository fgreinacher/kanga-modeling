using System;
using System.Collections.Generic;
using KangaModeling.Compiler.ClassDiagrams.Model;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.ClassDiagrams
{

    public static class PointExtensions
    {
        public static Point RotateAround(this Point p, Point rotationCenter, double angle)
        {
            var p1 = p.Subtract(rotationCenter);
            var p2 = p1.Rotate(angle);
            var p3 = p2.Add(rotationCenter);
            return p3;
            //return p.Subtract(rotationCenter).Rotate(angle).Add(rotationCenter);
        }

        public static Point Rotate(this Point p, double angle)
        {
            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);

            return new Point((float)(p.X * cosAngle + p.Y * sinAngle), (float)(-p.X * sinAngle + p.Y * cosAngle));
        }

        public static double CalculateAngle(this Point from, Point to)
        {
            // calculate the length of the vector between the two points
            var vector = to.Subtract(from);
            var length = Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));

            return Math.Acos((to.X - from.X) / length);
        }

        public static Point Add(this Point p, Point other)
        {
            return new Point(p.X + other.X, p.Y + other.Y);
        }

        public static Point Subtract(this Point p, Point other)
        {
            return new Point(p.X - other.X, p.Y - other.Y);
        }
    }

    /// <summary>
    /// Graphically represents an association
    /// </summary>
    public sealed class AssociationVisual : Visual
    {

        #region Drawing

        private abstract class Drawing
        {
            public abstract void Draw(IGraphicContext gc);
            public abstract void RotateAround(Point rotationCenter, double angle);
        }

        private sealed class SegmentDrawing : Drawing
        {
            public SegmentDrawing(Point from, Point to)
            {
                if (from == null) throw new ArgumentNullException("from");
                if (to == null) throw new ArgumentNullException("to");
                _from = from;
                _to = to;
            }

            public override void Draw(IGraphicContext gc)
            {
                gc.DrawLine(_from, _to, 2.0f, Color.Red, LineStyle.Sketchy);
            }

            public override void RotateAround(Point rotationCenter, double angle)
            {
                _from = _from.RotateAround(rotationCenter, angle);
                _to = _to.RotateAround(rotationCenter, angle);
            }


            private Point _from;
            private Point _to;
        }

        private sealed class PolygonDrawing : Drawing
        {
            private readonly Point[] _points;

            public PolygonDrawing(params Point[] points)
            {
                _points = points;
            }

            public override void Draw(IGraphicContext gc)
            {
                gc.FillPolygon(_points, Color.Red);
            }

            public override void RotateAround(Point rotationCenter, double angle)
            {
                for (int i = 0; i < _points.Length; i++)
                    _points[i] = _points[i].RotateAround(rotationCenter, angle);
            }
        }

        #endregion

        private readonly IAssociation _association;
        private readonly ClassDiagramVisual _classDiagramVisual;
        private readonly List<Drawing> _drawings;

        public AssociationVisual(IAssociation association, ClassDiagramVisual cdv)
        {
            if (association == null) throw new ArgumentNullException("association");
            if (cdv == null) throw new ArgumentNullException("cdv");
            _association = association;
            _classDiagramVisual = cdv;
            _drawings = new List<Drawing>();
            Location = Point.Empty;
        }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            // need to clear because LayoutCore is called twice for each run.
            _drawings.Clear();

            var sourceVisual = _classDiagramVisual.GetClassVisual(_association.Source);
            var targetVisual = _classDiagramVisual.GetClassVisual(_association.Target);

            // assume source class is on the left...
            switch (_association.Kind)
            {
                case AssociationKind.Undirected:
                    _drawings.Add(CalculateUnidirectedLineSegments(sourceVisual, targetVisual));
                    break;
                case AssociationKind.Directed:
                    CalculateDirectedLineSegments(sourceVisual, targetVisual);
                    break;
                case AssociationKind.Aggregation:
                    CalculateAggregationLineSegments(sourceVisual, targetVisual);
                    break;
                case AssociationKind.Composition:
                    CalculateCompositionPoints(sourceVisual, targetVisual);
                    break;
            }

            RotateLineSegments(sourceVisual, targetVisual);
        }

        private void CalculateCompositionPoints(ClassVisual sourceVisual, ClassVisual targetVisual)
        {
            CalculateDirectedLineSegments(sourceVisual, targetVisual, 20f);

            var startPoint = new Point(sourceVisual.Location.X + sourceVisual.Size.Width, sourceVisual.Location.Y + sourceVisual.Size.Height / 2f);

            var aggrUp = startPoint.Subtract(new Point(-10f, 10f));
            var aggrDown = startPoint.Add(new Point(10f, +10f));
            var aggrRight = startPoint.Add(new Point(20f, 0f));

            _drawings.Add(new PolygonDrawing(startPoint, aggrUp, aggrRight, aggrDown));
        }

        private void CalculateAggregationLineSegments(ClassVisual sourceVisual, ClassVisual targetVisual)
        {
            CalculateDirectedLineSegments(sourceVisual, targetVisual, 20f);

            var startPoint = new Point(sourceVisual.Location.X + sourceVisual.Size.Width, sourceVisual.Location.Y + sourceVisual.Size.Height / 2f);

            var aggrUp = startPoint.Subtract(new Point(-10f, 10f));
            var aggrDown = startPoint.Add(new Point(10f, +10f));
            var aggrRight = startPoint.Add(new Point(20f, 0f));

            _drawings.Add(new SegmentDrawing(startPoint, aggrUp));
            _drawings.Add(new SegmentDrawing(startPoint, aggrDown));
            _drawings.Add(new SegmentDrawing(aggrUp, aggrRight));
            _drawings.Add(new SegmentDrawing(aggrDown, aggrRight));
        }

        private void CalculateDirectedLineSegments(ClassVisual sourceVisual, ClassVisual targetVisual, float xOffset = 0f)
        {
            float commonY = sourceVisual.Location.Y + sourceVisual.Size.Height/2f;
            var startPoint = new Point(sourceVisual.Location.X + sourceVisual.Size.Width + xOffset, commonY);
            var commonYEndPoint= new Point(targetVisual.Location.X, commonY);

            var arrowPointUpper = new Point(commonYEndPoint.X - 10f, commonYEndPoint.Y - 10f);
            var arrowPointLower = new Point(commonYEndPoint.X - 10f, commonYEndPoint.Y + 10f);

            _drawings.Add(new SegmentDrawing(startPoint, commonYEndPoint));
            _drawings.Add(new SegmentDrawing(arrowPointLower, commonYEndPoint));
            _drawings.Add(new SegmentDrawing(arrowPointUpper, commonYEndPoint));
        }

        private void RotateLineSegments(ClassVisual sourceVisual, ClassVisual targetVisual)
        {
            var startPoint = new Point(sourceVisual.Location.X + sourceVisual.Size.Width, sourceVisual.Location.Y + sourceVisual.Size.Height / 2f);
            var trueEndPoint = new Point(targetVisual.Location.X, targetVisual.Location.Y + targetVisual.Size.Height / 2f);

            var rotationCenter = startPoint;
            var angle = startPoint.CalculateAngle(trueEndPoint);

            foreach (Drawing d in _drawings)
                d.RotateAround(rotationCenter, angle);
        }

        private SegmentDrawing CalculateUnidirectedLineSegments(ClassVisual sourceVisual, ClassVisual targetVisual)
        {
            float yLocation = sourceVisual.Location.Y + sourceVisual.Size.Height/2f;
            var startPoint = new Point(sourceVisual.Location.X + sourceVisual.Size.Width, yLocation);
            var endPoint = new Point(targetVisual.Location.X, yLocation);
            return new SegmentDrawing(startPoint, endPoint);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            _drawings.ForEach(ls => ls.Draw(graphicContext));
        }

    }
}