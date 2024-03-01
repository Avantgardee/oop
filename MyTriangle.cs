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

public class MyTriangle : MyPolygon
{
    // Конструктор с параметрами цвета заливки и обводки, и массива точек треугольника
    public MyTriangle(Brush fillColor, Brush strokeColor, Point[] points)
        : base(fillColor, strokeColor, points)
    {
        // Проверяем, что переданный массив точек содержит 3 точки
        if (points.Length == 3)
        {
            Points = points; // Присваиваем массив точек полю Points
        }
        else
        {
            throw new ArgumentException("Треугольник должен иметь 3 точки");
        }
    }

    public MyTriangle(Point[] points) : base(points)
    {
        if (points.Length == 3)
        {
            Points = points; // Присваиваем массив точек полю Points
        }
        else
        {
            throw new ArgumentException("Треугольник должен иметь 3 точки");
        }
    }

}