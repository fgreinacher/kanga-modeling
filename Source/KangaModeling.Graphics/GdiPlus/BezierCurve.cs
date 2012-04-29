using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Graphics.GdiPlus
{
    internal sealed class BezierCurve
    {
        private readonly Random m_Random = new Random();

        public BezierCurve(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;

            CalculateControlPoints();
        }

        private void CalculateControlPoints()
        {
            float lineLength = (float)Math.Sqrt(
                Math.Pow(EndPoint.X - StartPoint.X, 2) +
                Math.Pow(EndPoint.Y - StartPoint.Y, 2));

            int maximumMistake = (int)Math.Round(lineLength / 35);

            CalculateFirstControlPoint(maximumMistake);
            CalculateSecondControlPoint(maximumMistake);
        }

        private void CalculateFirstControlPoint(int maximumMistake)
        {
            float firstControlPointPosition = (float)m_Random.Next(20, 80) / 100;
            FirstControlPoint = new Point(
                StartPoint.X + (EndPoint.X - StartPoint.X) * firstControlPointPosition + m_Random.Next(0, maximumMistake),
                StartPoint.Y + (EndPoint.Y - StartPoint.Y) * firstControlPointPosition + m_Random.Next(0, maximumMistake));
        }

        private void CalculateSecondControlPoint(int maximumMistake)
        {
            float secondControlPointPosition = (float)m_Random.Next(20, 80) / 100;
            SecondControlPoint = new Point(
                StartPoint.X + (EndPoint.X - StartPoint.X) * secondControlPointPosition + m_Random.Next(-maximumMistake, 0),
                StartPoint.Y + (EndPoint.Y - StartPoint.Y) * secondControlPointPosition + m_Random.Next(-maximumMistake, 0));
        }

        public Point StartPoint { get; private set; }

        public Point EndPoint { get; private set; }

        public Point FirstControlPoint { get; private set; }

        public Point SecondControlPoint { get; private set; }
    }
}
