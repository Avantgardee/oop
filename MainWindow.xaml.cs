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
using OOTPiSP.Strategy;
using WpfApp2.FactoryMethods;
using System.Collections.Generic;
namespace WpfApp2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
   public ToolType[] ToolArr { get; set; } = new ToolType[7];
    static AbstractFactory Factory { get; set; } = new EllipseFactory();
    static AbstractDrawStrategy DrawStrategy { get; set; } = new EllipseDrawStrategy();
    
    public int click = 0;
    
    ToolType ToolNow = new ToolType(Factory, DrawStrategy, 2);
    
    List<Point> pointsList = new List<Point>();
    double rotationAngle = 0;
    bool isMove;
    bool isSet = false;
    private void ToolBtnClick(object sender, RoutedEventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            pointsList.Clear();
            int buttonIndex = int.Parse(button.Tag.ToString());
            ToolNow = ToolArr[buttonIndex];
        }
    }

    public MainWindow()
    {
        ToolArr[0] = ToolNow; // ellips
        ToolArr[1] = new ToolType(new CircleFactory(), new EllipseDrawStrategy(), 2); // circle
        ToolArr[2] = new ToolType(new BrokenLineFactory(), new BrokenLineDrawStrategy(), -1); // brokenline
        ToolArr[3] =  new ToolType(new PolygonFactory(), new PolygonDrawStrategy(),  -1); // polygon
        ToolArr[4] = new ToolType(new TriangleFactory(), new PolygonDrawStrategy(),  3); // triangle
        ToolArr[5] = new ToolType(new RectangleFactory(), new PolygonDrawStrategy(), 2 ); // rectangle
        ToolArr[6] = new ToolType(new SquareFactory(), new PolygonDrawStrategy(),  2); // square

        InitializeComponent();
    }
     void Canvas_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            int count = Canvas.Children.Count;
            rotationAngle = 0;
            if (count > 0)
                Canvas.Children.RemoveAt(count - 1);
        }

        void Canvas_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            click++;
                var mousePos = e.GetPosition(Canvas);
                pointsList.Add(mousePos);
                if (pointsList.Count == ToolNow.CountOfPoints)
                {
                    isMove = false;
                }
           
        }
        private void Canvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Проверяем, была ли нажата средняя кнопка мыши
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                if (ToolNow.CountOfPoints == -1)
                {
                    pointsList.Clear(); 
                }
            }
        }
        void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (ToolNow.CountOfPoints != -1)
            {
            
                int delta = e.Delta;

                if (delta > 0)
                {
              
                    rotationAngle += 10; // Увеличиваем угол на 10 градусов
                }
                else
                {
                    rotationAngle -= 10; 
                }
                rotationAngle %= 360; 
                if (rotationAngle < 0) rotationAngle += 360; 
                if (e.LeftButton == MouseButtonState.Pressed )
                {
                    var mousePosition = e.GetPosition(Canvas);
                    int count = Canvas.Children.Count; 
                    if (count > 0)
                        Canvas.Children.RemoveAt(count - 1);
               
                    Point[] pointsArray = pointsList.ToArray();
                    pointsArray[pointsArray.Length - 1] = mousePosition;
                    MySprite shape = ToolNow.Factory.CreateShape(FillColorPicker.SelectedBrush, PenColorPicker.SelectedBrush, pointsArray, rotationAngle);
                    ToolNow.Strategy.Draw(shape, Canvas);
                }
            }
            
           
        }
        void Canvas_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            
                if (e.LeftButton == MouseButtonState.Pressed ){
                    if (pointsList.Count == ToolNow.CountOfPoints)
                    {
                        var mousePosition = e.GetPosition(Canvas);
                        if (isMove)
                        {
                            int count = Canvas.Children.Count; 
                            if (count > 0)
                                Canvas.Children.RemoveAt(count - 1);
                        }
                        else
                        {
                            isMove = true;
                        }

                        Point[] pointsArray = pointsList.ToArray();
                        pointsArray[pointsArray.Length - 1] = mousePosition;
                        MySprite shape = ToolNow.Factory.CreateShape(FillColorPicker.SelectedBrush, PenColorPicker.SelectedBrush, pointsArray, rotationAngle);
                        ToolNow.Strategy.Draw(shape, Canvas);
                        pointsList[pointsList.Count - 1] = mousePosition;
                    }
                    
                    else if (ToolNow.CountOfPoints == -1 && pointsList.Count > 1)
                    {
                        var mousePosition = e.GetPosition(Canvas);
                        if (isMove)
                        {
                                Canvas.Children.RemoveAt(Canvas.Children.Count - 1);
                           
                        }
                        else
                        {
                            isMove = true;
                        }

                        
                        Point[] pointsArray = pointsList.ToArray();
                        pointsArray[pointsArray.Length - 1] = mousePosition;
                        MySprite shape = ToolNow.Factory.CreateShape(FillColorPicker.SelectedBrush, PenColorPicker.SelectedBrush, pointsArray, rotationAngle);
                        ToolNow.Strategy.Draw(shape, Canvas);
                        pointsList[pointsList.Count - 1] = mousePosition;
                    }
                }
                else
                {
                    if (pointsList.Count == ToolNow.CountOfPoints)
                    {
                        pointsList.Clear();
                    }
                }

                
            
        }
        

        void Canvas_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                    if (pointsList.Count == ToolNow.CountOfPoints)
                    {
                        var mousePosition = e.GetPosition(Canvas);
                   
                        isMove = false;
                        Point[] pointsArray = pointsList.ToArray();
                        pointsArray[pointsArray.Length - 1] = mousePosition;
                        pointsList[pointsList.Count - 1] = mousePosition;
                        MySprite shape = ToolNow.Factory.CreateShape(FillColorPicker.SelectedBrush, PenColorPicker.SelectedBrush,pointsArray,rotationAngle);
                        ToolNow.Strategy.Draw(shape, Canvas);
                        pointsList.Clear(); 
                        rotationAngle = 0;
                    }

                    if (ToolNow.CountOfPoints == -1 && pointsList.Count > 1)
                    {
                        isSet = true;
                        var mousePosition = e.GetPosition(Canvas);
                        int count = Canvas.Children.Count; 
                        if (count > 0 && pointsList.Count > 2)
                            Canvas.Children.RemoveAt(count - 1);
                        isMove = false;
                        Point[] pointsArray = pointsList.ToArray();
                        pointsArray[pointsArray.Length - 1] = mousePosition;
                        pointsList[pointsList.Count - 1] = mousePosition;
                        MySprite shape = ToolNow.Factory.CreateShape(FillColorPicker.SelectedBrush, PenColorPicker.SelectedBrush,pointsArray,rotationAngle);
                        ToolNow.Strategy.Draw(shape, Canvas);
                        rotationAngle = 0;
                    }
   
            }
        }
}