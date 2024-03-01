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
public class MyPolygon : MyShape
{
    // Конструктор с параметрами цвета заливки и обводки, и массива точек многоугольника
    public MyPolygon(System.Windows.Media.Brush fillColor, System.Windows.Media.Brush strokeColor, System.Windows.Point[] points)
        : base(fillColor, strokeColor, points)
    {
        // Проверка на количество точек
        // if (points.Length < 3)
        //     throw new ArgumentException("Polygon must have at least three points.", nameof(points));

    }

    // Конструктор без параметров (с голубыми цветами по умолчанию) и массива точек многоугольника
    public MyPolygon(System.Windows.Point[] points)
        : base(points)
    {
        // Проверка на количество точек
        // if (points.Length < 3)
        //     throw new ArgumentException("Polygon must have at least three points.", nameof(points));

    }

    // Реализация метода Draw для отрисовки многоугольника
    public override void Draw(Canvas canvas)
    {
        Polygon polygon = new Polygon();
        polygon.Fill = FillColor;
        polygon.Stroke = StrokeColor;
        polygon.Points = new PointCollection(Points);
        canvas.Children.Add(polygon);
    }

    // Метод для добавления точки в массив точек многоугольника
    protected void AddPoint(Point point)
    {
        List<Point> pointsList = new List<Point>(Points);
        pointsList.Add(point);
        Points = pointsList.ToArray();
    }

    // Метод для удаления последней точки из массива точек многоугольника
    protected void RemoveLastPoint()
    {
        if (Points.Length > 0)
        {
            List<Point> pointsList = new List<Point>(Points);
            pointsList.RemoveAt(pointsList.Count - 1);
            Points = pointsList.ToArray();
        }
    }
}