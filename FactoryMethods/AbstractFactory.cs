using System.Windows.Media;
namespace WpfApp2.FactoryMethods;
using System.Windows;

public abstract class AbstractFactory
{
    public abstract MySprite CreateShape(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle);
}