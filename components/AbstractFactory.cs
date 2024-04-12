using System.Windows.Media;
namespace components;
using System.Windows;

public abstract class AbstractFactory
{
    public abstract MySprite CreateShape(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle);
}