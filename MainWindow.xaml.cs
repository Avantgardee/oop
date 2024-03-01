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

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MyShapeList myShapeList = new MyShapeList();

        Point[] shesti =
        {
            new Point(50, 200), new Point(300 + 100 * Math.Sqrt(3), 300), new Point(100 * Math.Sqrt(3), 300),
            new Point(0, 200), new Point(100 * Math.Sqrt(3), 100), new Point(300 + 100 * Math.Sqrt(3), 100)
        };
        MyPolygon pol = new MyPolygon(Brushes.Brown, Brushes.Blue,20, shesti);
        myShapeList.AddShape(pol);
        
        MyCircle circle = new MyCircle(Brushes.Red, Brushes.Black,  new Point(100, 100),50 );
        myShapeList.AddShape(circle);

        MyRectangle rectangle = new MyRectangle(Brushes.Red, Brushes.Black, 45,new Point(400, 200),50,20 );
        
        myShapeList.AddShape(rectangle);

        Point[] trianglePoints = { new Point(60, 100), new Point(400, 200), new Point(200, 200) };
        MyTriangle triangle = new MyTriangle(Brushes.Green, Brushes.Black,60, trianglePoints);
        myShapeList.AddShape(triangle);
        
        MySquare square = new MySquare(new Point(500,100), 100, 45,Brushes.Green, Brushes.Black);
        
        myShapeList.AddShape(square);

        MyEllipse el = new MyEllipse(Brushes.Chartreuse, Brushes.Red, new Point(600, 300), 40, 80);
        myShapeList.AddShape(el);
        
        Point[] brokPoints =
        {
            new Point(420, 67),
            new Point(599, 254),
            new Point(302, 106),
            new Point(734, 357),
            new Point(648, 180),
            new Point(187, 412)
        };
        MyBrokenLine brok = new MyBrokenLine(Brushes.Indigo,Brushes.Indigo, brokPoints);
        myShapeList.AddShape(brok);
        
        myShapeList.DrawAll(Canvas);
    }
}