using System.Windows.Media;
using System.Windows;
namespace WpfApp2.FactoryMethods;

public class CircleFactory: AbstractFactory{
    public override MySprite CreateShape(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle)
    {
        return new MyCircle(fillColor, strokeColor, points, rotationAngle);
    }
    
}