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

public abstract class MySprite
{
    public Brush FillColor { get; set; }
    public Brush StrokeColor { get; set; }

   
    public MySprite(Brush fillColor, Brush strokeColor)
    {
        FillColor = fillColor;
        StrokeColor = strokeColor;
    }

    public MySprite()
    {
        FillColor = Brushes.LightBlue;
        StrokeColor = Brushes.Blue;
    }

   
    public abstract void Draw(Canvas canvas);
}