using System.Windows;
using System.Windows.Media;
using components;
using Star;

namespace Heart;

public class FactoryForStar : AbstractFactory
{
    public override MySprite CreateShape(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle)
    {
        return new StarShape(fillColor, strokeColor, points, rotationAngle);
    }
}