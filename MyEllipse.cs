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

public class MyEllipse : MySprite
{
    public Point Center { get; set; }
    public double RadiusX { get; set; }
    public double RadiusY { get; set; }

    // Конструктор с параметрами цвета заливки и обводки, центра и радиусов эллипса
    public MyEllipse(Brush fillColor, Brush strokeColor, Point center, double radiusX, double radiusY)
        : base(fillColor, strokeColor)
    {
        Center = center;
        RadiusX = radiusX;
        RadiusY = radiusY;
    }

    // Конструктор без параметров (с голубыми цветами по умолчанию) и центра и радиусов эллипса
    public MyEllipse(Point center, double radiusX, double radiusY)
        : base()
    {
        Center = center;
        RadiusX = radiusX;
        RadiusY = radiusY;
    }

    // Реализация метода Draw для отрисовки эллипса
    public override void Draw(Canvas canvas)
    {
        Ellipse ellipse = new Ellipse();
        ellipse.Fill = FillColor;
        ellipse.Stroke = StrokeColor;
        ellipse.Width = 2 * RadiusX;
        ellipse.Height = 2 * RadiusY;
        Canvas.SetLeft(ellipse, Center.X - RadiusX);
        Canvas.SetTop(ellipse, Center.Y - RadiusY);
        canvas.Children.Add(ellipse);
    }
}