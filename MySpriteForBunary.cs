using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using components;
namespace WpfApp2;
[Serializable]
public class MySpriteForBinary
{
    public object idOfClassShape { get; set; }

    public double Angle { get; set; }

    public Point[] Points { get; set; }
    
    public double StrokeThickness { get; set; }
    
    
    public string BackgroundColor { get; set; }

    
    public string PenColor { get; set; }
    
}