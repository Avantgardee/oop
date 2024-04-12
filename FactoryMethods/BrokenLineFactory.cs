using System.Windows.Media;
using System.Windows;
using components;
namespace WpfApp2.FactoryMethods;

public class BrokenLineFactory: AbstractFactory
{
    public override MySprite CreateShape(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle)
    {
        return new MyBrokenLine(fillColor, strokeColor, points, rotationAngle);
    }
}