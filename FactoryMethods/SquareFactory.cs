using System.Windows.Media;
using System.Windows;
using components;
namespace WpfApp2.FactoryMethods;

public class SquareFactory: AbstractFactory
{
    public override MySprite CreateShape(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle)
    {
        return new MySquare(fillColor, strokeColor, points, rotationAngle);
    }
}







