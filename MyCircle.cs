using System.Text;
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

public class MyCircle : MyEllipse
{
    public override object TagShape => "1";
    public MyCircle(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle)
        : base(fillColor, strokeColor, points, rotationAngle)
    {
        Point topLeft = points[0];
        Point bottomRight = points[1];
        double centerX = (topLeft.X + bottomRight.X) / 2;
        double centerY = ((bottomRight.X - topLeft.X)/ 2 +  topLeft.Y);
        Center = new Point(centerX, centerY);
        RadiusX  = Math.Abs(topLeft.X - Center.X);
        RadiusY = RadiusX;

        Points = new Point[] { topLeft, new Point(topLeft.X + 2 * RadiusX, topLeft.Y + 2 * RadiusX) };
        CalculateCenter();
    }
}