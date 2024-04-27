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
using components;
using WpfApp2.StrategyDraw;

namespace WpfApp2;

public class MyPolygon : MyShape
{
    public MyPolygon(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle)
        : base(fillColor, strokeColor, points, rotationAngle)
    {
        CalculateCenter();
        DrawStrategy = new PolygonDrawStrategy();
    }
    public override int countOfPoints => -1;
    public override object idOfClassShape => "3";
    
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
    public override string ToString() =>
        $"Полигон";
}