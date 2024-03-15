using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using WpfApp2;

namespace OOTPiSP.Strategy;

public interface AbstractDrawStrategy
{
    void Draw(MySprite sprite, Canvas canvas);
}