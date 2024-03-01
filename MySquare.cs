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

public class MySquare : MyRectangle
{
    
    public MySquare(Point topLeftPoint, double sideLength)
        : base(topLeftPoint, sideLength, sideLength)
    {
        _rotationAngle = 0;
        CalculateCenter();
    }
    public MySquare(Point topLeftPoint, double sideLength, double rotationAngle, Brush fillColor, Brush strokeColor)
        : base(fillColor, strokeColor,rotationAngle, topLeftPoint,sideLength,sideLength)
    {
        _rotationAngle = rotationAngle;
        CalculateCenter();
    }
    
    
}