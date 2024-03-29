using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using WpfApp2;

namespace OOTPiSP.Strategy;

public class BrokenLineDrawStrategy : AbstractDrawStrategy
{
    public Shape Draw(MySprite sprite, Canvas canvas)
    {
        if (sprite is MyBrokenLine myBrokenLine)
        {
            Polyline polyline = new Polyline();
            polyline.Stroke = myBrokenLine.StrokeColor;
            polyline.Points = myBrokenLine.RotatePoints(myBrokenLine.Points, myBrokenLine._rotationAngle);
            polyline.StrokeThickness = myBrokenLine.StrokeThickness;
            
            return polyline;
        }
        return null;
    }
}