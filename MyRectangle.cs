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


public class MyRectangle : MyPolygon
{
    // Конструктор с параметрами цвета заливки и обводки, верхней левой точки, ширины и длины прямоугольника
    public MyRectangle(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle)
        : base(fillColor, strokeColor,points, rotationAngle)
    {
        if (points.Length < 3)
        {
            AddRectanglePoints(points[0], points[1]);
        }
        CalculateCenter();
    }

    public override object TagShape => "5";
    protected virtual void AddRectanglePoints(Point topLeftPoint, Point bottomRightPoint)
    {
        RemoveLastPoint();
        RemoveLastPoint();
        // Добавляем верхнюю левую точку
        AddPoint(topLeftPoint);

        // Добавляем верхнюю правую точку
        Point topRightPoint = new Point(bottomRightPoint.X, topLeftPoint.Y);
        AddPoint(topRightPoint);

        // Добавляем нижнюю правую точку
        AddPoint(bottomRightPoint);

        // Добавляем нижнюю левую точку
        Point bottomLeftPoint = new Point(topLeftPoint.X, bottomRightPoint.Y);
        AddPoint(bottomLeftPoint);
    }
    
   
}