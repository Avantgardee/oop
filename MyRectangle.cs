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


public class MyRectangle : MyPolygon
{
    // Конструктор с параметрами цвета заливки и обводки, верхней левой точки, ширины и длины прямоугольника
    public MyRectangle(Brush fillColor, Brush strokeColor, Point topLeftPoint, double width, double height)
        : base(fillColor, strokeColor, new Point[0])
    {
        AddRectanglePoints(topLeftPoint, width, height);
    }

    // Конструктор без параметров (с голубыми цветами по умолчанию), верхней левой точки, ширины и длины прямоугольника
    public MyRectangle(Point topLeftPoint, double width, double height)
        : base(new Point[0])
    {
        AddRectanglePoints(topLeftPoint, width, height);
    }

    // Метод для добавления точек прямоугольника в массив точек
    private void AddRectanglePoints(Point topLeftPoint, double width, double height)
    {
        // Добавляем верхнюю левую точку
        AddPoint(topLeftPoint);

        // Добавляем верхнюю правую точку
        Point topRightPoint = new Point(topLeftPoint.X + width, topLeftPoint.Y);
        AddPoint(topRightPoint);

        // Добавляем нижнюю правую точку
        Point bottomRightPoint = new Point(topLeftPoint.X + width, topLeftPoint.Y + height);
        AddPoint(bottomRightPoint);

        // Добавляем нижнюю левую точку
        Point bottomLeftPoint = new Point(topLeftPoint.X, topLeftPoint.Y + height);
        AddPoint(bottomLeftPoint);
    }
    
   
}