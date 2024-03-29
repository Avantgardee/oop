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

public class MyBrokenLine : MyShape
{
    public override object TagShape => "2";
    public MyBrokenLine(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle)
        : base(fillColor, strokeColor, points, rotationAngle)
    {
       
        if (points.Length < 2)
            throw new ArgumentException("Broken line must have at least two points.", nameof(points));
        CalculateCenter();
    }

    
}
