using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using WpfApp2;

namespace OOTPiSP.Strategy;

public interface AbstractDrawStrategy
{
    
    Shape Draw(MySprite sprite, Canvas canvas);
}