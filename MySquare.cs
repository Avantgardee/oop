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

public class MySquare : MyRectangle
{
    public override object TagShape => "6";
    public MySquare( Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle)
        : base(fillColor, strokeColor, points, rotationAngle)
    {
        AddRectanglePoints(points[0], points[1]);
        CalculateCenter();
    }
    protected override void AddRectanglePoints(Point topLeftPoint, Point bottomRightPoint)
    {
        Points = new Point[0];
        // Добавляем верхнюю левую точку
        AddPoint(topLeftPoint);

        // Добавляем верхнюю правую точку
        Point topRightPoint = new Point(bottomRightPoint.X, topLeftPoint.Y);
        AddPoint(topRightPoint);

        // Добавляем нижнюю правую точку
        Point newBottomRightPoint = new Point(bottomRightPoint.X, topLeftPoint.Y + Math.Abs(bottomRightPoint.X - topLeftPoint.X));
        AddPoint(newBottomRightPoint);

        // Добавляем нижнюю левую точку
        Point bottomLeftPoint = new Point(topLeftPoint.X, newBottomRightPoint.Y);
        AddPoint(bottomLeftPoint);
    }
}