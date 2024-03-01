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
 
    public MyBrokenLine(Brush fillColor, Brush strokeColor, Point[] points)
        : base(fillColor, strokeColor, points)
    {
       
        if (points.Length < 2)
            throw new ArgumentException("Broken line must have at least two points.", nameof(points));
    }


    public MyBrokenLine(Point[] points)
        : base(points)
    {
      
        if (points.Length < 2)
            throw new ArgumentException("Broken line must have at least two points.", nameof(points));
    }


    public override void Draw(Canvas canvas)
    {
        Polyline polyline = new Polyline();
        polyline.Stroke = StrokeColor;
        polyline.Points = new PointCollection(Points);
        canvas.Children.Add(polyline);
    }
}