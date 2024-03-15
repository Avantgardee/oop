
using System.Windows.Media;
using System.Windows;
namespace WpfApp2.FactoryMethods;

public class TriangleFactory: AbstractFactory
{
    public override MySprite CreateShape(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle)
    {
        return new MyTriangle(fillColor, strokeColor, points, rotationAngle);
    }
}








