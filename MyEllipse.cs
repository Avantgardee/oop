using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2;
[Serializable]
public class MyEllipse : MySprite
{
    public Point Center { get; set; }
    public double RadiusX { get; set; }
    public double RadiusY { get; set; }

    public override object TagShape => "0";
    public MyEllipse(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle)
        : base(fillColor, strokeColor, points, rotationAngle)
    {
        Point topLeft = points[0];
        Point bottomRight = points[1];
        
        // Calculate center
        double centerX = (topLeft.X + bottomRight.X) / 2;
        double centerY = (topLeft.Y + bottomRight.Y) / 2;
        Center = new Point(centerX, centerY);

        // Calculate radiusX
        RadiusX = Math.Abs(bottomRight.X - Center.X);

        // Calculate radiusY
        RadiusY = Math.Abs(topLeft.Y - Center.Y);
        CalculateCenter();
        
    }
    
    
}