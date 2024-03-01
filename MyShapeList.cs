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

public class MyShapeList
{
    private List<MySprite> shapes; 

   
    public MyShapeList()
    {
        shapes = new List<MySprite>(); 
    }

   
    public void AddShape(MySprite mySprite)
    {
        shapes.Add(mySprite); 
    }

    
    public void DrawAll(Canvas canvas)
    {
        shapes.ForEach(shape => shape.Draw(canvas));
    }
}