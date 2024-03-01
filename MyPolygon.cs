using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2;
public class MyPolygon : MyShape
{
    
    protected double _rotationAngle;
    protected Point _center;

    
    public MyPolygon(Brush fillColor, Brush strokeColor, double rotationAngle, Point[] points)
        : base(fillColor, strokeColor, points)
    {
        _rotationAngle = rotationAngle;
        CalculateCenter();
    }


    public MyPolygon(Point[] points)
        : base(points)
    {
        _rotationAngle = 0;
        CalculateCenter();
    }
    protected void CalculateCenter()
    {
        double totalX = 0;
        double totalY = 0;

        foreach (var point in Points)
        {
            totalX += point.X;
            totalY += point.Y;
        }

        _center = new Point(totalX / Points.Length, totalY / Points.Length);
    }
   
    public override void Draw(Canvas canvas)
    {
        Polygon polygon = new Polygon();
        polygon.Fill = FillColor;
        polygon.Stroke = StrokeColor;
        polygon.Points = RotatePoints(Points, _rotationAngle);
        canvas.Children.Add(polygon);
    }

    protected PointCollection RotatePoints(Point[] points, double angle)
    {
        double angleInRadians = angle * Math.PI / 180.0;
        PointCollection rotatedPoints = new PointCollection();

        double cosAngle = Math.Cos(angleInRadians);
        double sinAngle = Math.Sin(angleInRadians);

        foreach (var point in points)
        {
            double rotatedX = _center.X + (point.X - _center.X) * cosAngle - (point.Y - _center.Y) * sinAngle;
            double rotatedY = _center.Y + (point.X - _center.X) * sinAngle + (point.Y - _center.Y) * cosAngle;
            rotatedPoints.Add(new Point(rotatedX, rotatedY));
        }

        return rotatedPoints;
    }
    protected void AddPoint(Point point)
    {
        List<Point> pointsList = new List<Point>(Points);
        pointsList.Add(point);
        Points = pointsList.ToArray();
    }

    
    protected void RemoveLastPoint()
    {
        if (Points.Length > 0)
        {
            List<Point> pointsList = new List<Point>(Points);
            pointsList.RemoveAt(pointsList.Count - 1);
            Points = pointsList.ToArray();
        }
    }
}