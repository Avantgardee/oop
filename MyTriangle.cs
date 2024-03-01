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

public class MyTriangle : MyPolygon
{
   
    public MyTriangle(Brush fillColor, Brush strokeColor,double rotationAngle, Point[] points)
        : base(fillColor, strokeColor,rotationAngle, points)
    {
        
        if (points.Length == 3)
        {
            Points = points; 
        }
        else
        {
            throw new ArgumentException("Треугольник должен иметь 3 точки");
        }
    }

    public MyTriangle(Point[] points) : base(points)
    {
        if (points.Length == 3)
        {
            Points = points;
        }
        else
        {
            throw new ArgumentException("Треугольник должен иметь 3 точки");
        }
    }

}