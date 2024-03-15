using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using WpfApp2;

namespace OOTPiSP.Strategy;

public class EllipseDrawStrategy: AbstractDrawStrategy
{
    public void Draw(MySprite sprite, Canvas canvas)
    {
        if (sprite is MyEllipse myElllipse)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = myElllipse.FillColor;
            ellipse.Stroke = myElllipse.StrokeColor;
            ellipse.Width = 2 * myElllipse.RadiusX;
            ellipse.Height = 2 * myElllipse.RadiusY;
            ellipse.RenderTransformOrigin = new Point(0.5, 0.5);
            RotateTransform rotateTransform = new RotateTransform(myElllipse._rotationAngle);
            ellipse.RenderTransform = rotateTransform;
            Canvas.SetLeft(ellipse, myElllipse.Center.X - myElllipse.RadiusX);
            Canvas.SetTop(ellipse, myElllipse.Center.Y - myElllipse.RadiusY);
            canvas.Children.Add(ellipse);
        }
    }
}