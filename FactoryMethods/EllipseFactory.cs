using System.Windows.Media;
namespace WpfApp2.FactoryMethods;
using System.Windows;
public class EllipseFactory: AbstractFactory
{
    public override MySprite CreateShape(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle)
    {
        return new MyEllipse(fillColor, strokeColor, points, rotationAngle);
    }
}