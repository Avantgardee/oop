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
    protected Point[] Points { get; set; }

    
    public MyShape(Brush fillColor, Brush strokeColor, Point[] points)
        : base(fillColor, strokeColor)
    {
        Points = points ?? throw new ArgumentNullException(nameof(points));
    }

   
    public MyShape(Point[] points)
        : base()
    {
        Points = points ?? throw new ArgumentNullException(nameof(points));
    }
}