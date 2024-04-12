using System.Windows.Media;
using System.Xml.Serialization;
using System.Windows;
using components;
namespace WpfApp2;

public class MySpriteForXML
{
    public object idOfClassShape { get; set; }

    public double Angle { get; set; }

    public Point[] Points { get; set; }
    
    public double StrokeThickness { get; set; }
    
    [XmlIgnore]
    public Brush BackgroundColor { get; set; }

    [XmlIgnore]
    public Brush PenColor { get; set; }

  
    public string BackgroundColorString
    {
        get => BackgroundColor.ToString();
        set => BackgroundColor = (SolidColorBrush)new BrushConverter().ConvertFromString(value);
    }

    public string PenColorString
    {
        get => PenColor.ToString();
        set => PenColor = (SolidColorBrush)new BrushConverter().ConvertFromString(value);
    }
}