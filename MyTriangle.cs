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
namespace WpfApp2;

public class MyTriangle : MyPolygon
{
    public override object idOfClassShape => "4";
    public override int countOfPoints => 3;
    public MyTriangle(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle)
        : base(fillColor, strokeColor,points,rotationAngle)
    {
        
        // if (points.Length == 3)
        // {
        //     Points = points; 
        // }
        // else
        // {
        //     throw new ArgumentException("Треугольник должен иметь 3 точки");
        // }
        CalculateCenter();
    }
    public override string ToString() =>
        $"Треугольник";

}