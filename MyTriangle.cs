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
[Serializable]
public class MyTriangle : MyPolygon
{
    public override object TagShape => "4";
    public MyTriangle(Brush fillColor, Brush strokeColor, Point[] points, double rotationAngle)
        : base(fillColor, strokeColor,points,rotationAngle)
    {
        
        if (points.Length == 3)
        {
            Points = points; 
        }
        else
        {
            throw new ArgumentException("Треугольник должен иметь 3 точки");
        }
        CalculateCenter();
    }
    

}