using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using components;

namespace Star
{
    public class StrategyForStar : AbstractDrawStrategy
    {
        public Shape Draw(MySprite sprite, Canvas canvas)
        {
            if (sprite is StarShape star)
            {
                double width = Math.Abs(star.Points[0].X - star.Points[1].X);
                double height = Math.Abs(star.Points[0].Y - star.Points[1].Y);

                double centerX = star.Points[0].X + width / 2;
                double centerY = star.Points[0].Y + height / 2;

                double outerRadius = Math.Min(width, height) / 2;
                double innerRadius = outerRadius * 0.382; // Golden ratio for aesthetically pleasing stars

                Point[] outerPoints = new Point[10];
                Point[] innerPoints = new Point[10];

                for (int i = 0; i < 10; i++)
                {
                    double angleOuter = i * Math.PI / 5;
                    double angleInner = (i + 0.5) * Math.PI / 5;

                    outerPoints[i] = new Point(centerX + outerRadius * Math.Cos(angleOuter), centerY + outerRadius * Math.Sin(angleOuter));
                    innerPoints[i] = new Point(centerX + innerRadius * Math.Cos(angleInner), centerY + innerRadius * Math.Sin(angleInner));
                }

                PathGeometry starGeometry = new PathGeometry();
                PathFigure starFigure = new PathFigure { StartPoint = outerPoints[0] };
                for (int i = 0; i < 10; i++)
                {
                    int outerIndex = i % 10;
                    int innerIndex = (i + 5) % 10;

                    starFigure.Segments.Add(new LineSegment(outerPoints[outerIndex], true));
                    starFigure.Segments.Add(new LineSegment(innerPoints[innerIndex], true));
                }
                starFigure.IsClosed = true;
                starGeometry.Figures.Add(starFigure);

                var path = new Path
                {
                    Stroke = star.StrokeColor,
                    Fill = star.FillColor,
                    Data = starGeometry,
                    StrokeThickness = star.StrokeThickness,
                    RenderTransform = new RotateTransform(star._rotationAngle, centerX, centerY),
                };

                return path;
            }

            return null;
        }
    }
}