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
    // Конструктор с параметрами цвета заливки и обводки, и массива точек ломанной линии
    public MyBrokenLine(Brush fillColor, Brush strokeColor, Point[] points)
        : base(fillColor, strokeColor, points)
    {
        // Проверка на количество точек
        if (points.Length < 2)
            throw new ArgumentException("Broken line must have at least two points.", nameof(points));
    }

    // Конструктор без параметров (с голубыми цветами по умолчанию) и массива точек ломанной линии
    public MyBrokenLine(Point[] points)
        : base(points)
    {
        // Проверка на количество точек
        if (points.Length < 2)
            throw new ArgumentException("Broken line must have at least two points.", nameof(points));
    }

    // Реализация метода Draw для отрисовки ломанной линии
    public override void Draw(Canvas canvas)
    {
        Polyline polyline = new Polyline();
        polyline.Stroke = StrokeColor;
        polyline.Points = new PointCollection(Points);
        canvas.Children.Add(polyline);
    }
}