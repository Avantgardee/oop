using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;


namespace components;

public interface AbstractDrawStrategy
{
    
    Shape Draw(MySprite sprite, Canvas canvas);
}