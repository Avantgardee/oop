using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using WpfApp2;

namespace OOTPiSP.Strategy;

public class PolygonDrawStrategy : AbstractDrawStrategy
{
    public Shape Draw(MySprite sprite, Canvas canvas)
    {
        if (sprite is MyPolygon myPolygon)
        {
            Polygon polygon = new Polygon();
            polygon.Fill = myPolygon.FillColor;
            polygon.Stroke = myPolygon.StrokeColor;
            polygon.StrokeThickness = myPolygon.StrokeThickness;
            polygon.Points = myPolygon.RotatePoints(myPolygon.Points, myPolygon._rotationAngle);
            
            return polygon;
        }

        return null;
    }
}

