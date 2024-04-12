using System.Windows;
using System.Windows.Media;
using components;

namespace Star;

public class StarShape : MySprite
{
    public override object idOfClassShape { get; } = "Star";

    public StarShape( Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle) : base(fillColor,
        strokeColor, points, rotationAngle)
    {
        DrawStrategy = new StrategyForStar();
    }
    
    public override string ToString() =>
        $"Звезда";
}