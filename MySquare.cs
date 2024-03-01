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
    // Конструктор с параметрами верхней левой точки и длины стороны квадрата
    public MySquare(Point topLeftPoint, double sideLength)
        : base(topLeftPoint, sideLength, sideLength)
    {
    }
    public MySquare(Point topLeftPoint, double sideLength, Brush fillColor, Brush strokeColor)
        : base(fillColor, strokeColor,topLeftPoint,sideLength,sideLength)
    {
    }
    
    
}