using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp2;

namespace WpfApp2;
public partial class ShapeEditorWindow
{
    public MySprite Shape
    {
        get => (MySprite)GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    public static readonly DependencyProperty ShapeProperty;

    static ShapeEditorWindow()
    {
        ShapeProperty = DependencyProperty.Register(
            "Shape",
            typeof(MySprite),
            typeof(ShapeEditorWindow));
    }

    public ShapeEditorWindow(MySprite shape)
    {
        InitializeComponent();
        Shape = shape;
        
    }

    void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        double topShift = double.Parse(Top.Text);
        double bottomShift = double.Parse(Bottom.Text);
        double rightShift = double.Parse(Right.Text);
        double leftShift = double.Parse(Left.Text);
       
      
        Point[] shiftedPoints = new Point[Shape.Points.Length];
        for (int i = 0; i < Shape.Points.Length; i++)
        {
            Point originalPoint = Shape.Points[i];
            Point shiftedPoint = new Point(originalPoint.X + rightShift - leftShift, originalPoint.Y + bottomShift - topShift);
            shiftedPoints[i] = shiftedPoint;
        }
        Shape.Points = shiftedPoints;
        
    }
}