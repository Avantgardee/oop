using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace WpfApp2;
[Serializable]
public class MySpriteForBunary
{
    public object TagShape { get; set; }

    public double Angle { get; set; }

    public Point[] Points { get; set; }
    
    public double StrokeThickness { get; set; }
    
    
    public string BackgroundColor { get; set; }

    
    public string PenColor { get; set; }

    // //For XML
    // public string BackgroundColorString
    // {
    //     get => BackgroundColor.ToString();
    //     set => BackgroundColor = (SolidColorBrush)new BrushConverter().ConvertFromString(value); 
    // }
    //
    // public string PenColorString
    // {
    //     get => PenColor.ToString();
    //     set => PenColor = (SolidColorBrush)new BrushConverter().ConvertFromString(value);
    // }
}