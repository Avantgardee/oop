using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using WpfApp2;

namespace OOTPiSP.Strategy;

public class EllipseDrawStrategy: AbstractDrawStrategy
{
    public Shape Draw(MySprite sprite, Canvas canvas)
    {
        if (sprite is MyEllipse myElllipse)
        {
            MyEllipse tempShape = new MyEllipse(myElllipse.FillColor, myElllipse.StrokeColor, myElllipse.Points, myElllipse._rotationAngle);
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = tempShape.FillColor;
            ellipse.Stroke = tempShape.StrokeColor;
            ellipse.Width = 2 * tempShape.RadiusX;
            ellipse.Height = 2 * tempShape.RadiusY;
            ellipse.RenderTransformOrigin = new Point(0.5, 0.5);
            RotateTransform rotateTransform = new RotateTransform(tempShape._rotationAngle);
            ellipse.RenderTransform = rotateTransform;
            ellipse.StrokeThickness = tempShape.StrokeThickness;
            Canvas.SetLeft(ellipse, tempShape.Center.X - tempShape.RadiusX);
            Canvas.SetTop(ellipse, tempShape.Center.Y - tempShape.RadiusY);
            
            return ellipse;
        }

        return null;
    }
}