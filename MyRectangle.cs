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
    public MyRectangle(Brush fillColor, Brush strokeColor, double rotationAngle,  Point topLeftPoint, double width, double height)
        : base(fillColor, strokeColor,rotationAngle, new Point[0])
    {
        AddRectanglePoints(topLeftPoint, width, height);
        _rotationAngle = rotationAngle;
        CalculateCenter();
    }

    
    public MyRectangle(Point topLeftPoint, double width, double height)
        : base(new Point[0])
    {
        AddRectanglePoints(topLeftPoint, width, height);
        _rotationAngle = 0;
        CalculateCenter();
    }

   
    private void AddRectanglePoints(Point topLeftPoint, double width, double height)
    {
        
        AddPoint(topLeftPoint);

        
        Point topRightPoint = new Point(topLeftPoint.X + width, topLeftPoint.Y);
        AddPoint(topRightPoint);

   
        Point bottomRightPoint = new Point(topLeftPoint.X + width, topLeftPoint.Y + height);
        AddPoint(bottomRightPoint);

      
        Point bottomLeftPoint = new Point(topLeftPoint.X, topLeftPoint.Y + height);
        AddPoint(bottomLeftPoint);
    }
    
   
}