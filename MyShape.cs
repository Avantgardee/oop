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

public abstract class MyShape : MySprite
{
    public Point[] Points { get; set; }

    // Конструктор с параметрами цвета заливки и обводки
    public MyShape(Brush fillColor, Brush strokeColor, Point[] points)
        : base(fillColor, strokeColor)
    {
        Points = points ?? throw new ArgumentNullException(nameof(points));
    }

    // Конструктор без параметров (с голубыми цветами по умолчанию)
    public MyShape(Point[] points)
        : base()
    {
        Points = points ?? throw new ArgumentNullException(nameof(points));
    }
}