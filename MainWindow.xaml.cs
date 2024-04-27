using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.StrategyDraw;
using WpfApp2.FactoryMethods;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Microsoft.Win32;
using components;
namespace WpfApp2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
   //public ToolType[] ToolArr { get; set; } = new ToolType[7];
   private Dictionary<object, ToolType> ToolArr = new Dictionary<object, ToolType>();
    static AbstractFactory Factory { get; set; } = new EllipseFactory();
    
    static AbstractDrawStrategy DrawStrategy { get; set; } = new EllipseDrawStrategy();
    
    ToolType ToolNow = new ToolType(Factory, DrawStrategy, 2);
    
    List<Point> pointsList = new List<Point>();
    double rotationAngle = 0;
    bool isMove;
    List<MySprite> SpritesList { get; set; } = [];
    Dictionary<string, Tuple<bool, IPluginFunctionality>> CurrentFunctionalPlugins = new Dictionary<string, Tuple<bool, IPluginFunctionality>>();
    private void ToolBtnClick(object sender, RoutedEventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            pointsList.Clear();
            object buttonIndex = button.Tag;
            ToolNow = ToolArr[buttonIndex];
            rotationAngle = 0;
        }
    }
    
    public MainWindow()
    {
        
        InitializeComponent();
        List<AbstractFactory> list = [];
        var context = new AssemblyLoadContext("DynamicLoad", true);
        Assembly assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(AbstractFactory)));
        foreach (var type in types)
        {
            if (Activator.CreateInstance(type) is AbstractFactory factory)
            {
                list.Add(factory);
            }
        }
        context.Unload();
        foreach (var factory in list)
        {
            Point[] temp =  { new Point(0, 0), new Point(0, 0)};
            var shape = factory.CreateShape(Brushes.Black,Brushes.Black ,temp, 0);
            if (!ToolArr.ContainsKey(shape.idOfClassShape))
            {
                ToolArr.Add(shape.idOfClassShape, new ToolType(factory,shape.DrawStrategy,shape.countOfPoints));
            }


            Button newButton = new Button
            {
                Content = shape.ToString(),
                Tag = shape.idOfClassShape,
            };
            newButton.Click += ToolBtnClick;
                    
            int rowIndex = ButtonGrid.RowDefinitions.Count;
            ButtonGrid.RowDefinitions.Add(
                new()
                {
                    Height = new GridLength(1, GridUnitType.Star),
                });
            Grid.SetRow(newButton, rowIndex);
            ButtonGrid.Children.Add(newButton);
            ToolNow = ToolArr[shape.idOfClassShape];
        }
        
    }
        
    public void DrawShapeOnCanvas(Canvas canvas, MySprite sprite)
    {
        Shape drawnShape = ToolNow.Strategy.Draw(sprite, canvas);
        if (drawnShape != null)
        {
            if (sprite.CanvasIndex < 0)
            {
                sprite.CanvasIndex = canvas.Children.Count;
                canvas.Children.Add(drawnShape);
            }
            else
            {
                canvas.Children.RemoveAt(sprite.CanvasIndex);
                canvas.Children.Insert(sprite.CanvasIndex, drawnShape);
            }
            drawnShape.Tag = sprite.CanvasIndex;
        }
    }
    void RmvLstShp()
    {
        int count = Canvas.Children.Count;
        if (count > 0)
        {
            Canvas.Children.RemoveAt(count - 1);
            SpritesList.RemoveAt(count - 1);
        }

    }
     void Canvas_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            rotationAngle = 0;
            if (e.ClickCount == 2)
            {
                RmvLstShp();
                return;
            }
            if (e is { ClickCount: 1, OriginalSource: Shape shape })
            {
                int shapeTag = (int)shape.Tag;
                for (int i = shapeTag + 1; i < Canvas.Children.Count; i++)
                {
                    if (Canvas.Children[i] is Shape item)
                    {
                        int tagTemp = (int)item.Tag;
                        item.Tag = --tagTemp;
                    }
                }
                Canvas.Children.RemoveAt(shapeTag);

                for (int i = shapeTag + 1; i < SpritesList.Count; i++)
                {
                    SpritesList[i].CanvasIndex--;
                }

                SpritesList.RemoveAt(shapeTag);
            }
        }

       
        void Canvas_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e is { ClickCount: 2, Source: Shape frameworkElement })
            {
                var tag = (int) frameworkElement.Tag;

                var shape = SpritesList[tag];
                ToolNow = ToolArr[shape.idOfClassShape];
                var shapeEditorWindow = new ShapeEditorWindow(shape);
                shapeEditorWindow.ShowDialog();
            
                DrawShapeOnCanvas(Canvas, shape);

                Canvas.Children[shape.CanvasIndex].PreviewMouseUp += Canvas_OnMouseUp;
                Canvas.Children[shape.CanvasIndex].PreviewMouseWheel += Window_MouseWheel;
                Canvas.Children[shape.CanvasIndex].MouseEnter += Shape_MouseEnter;
                Canvas.Children[shape.CanvasIndex].MouseLeave += Shape_MouseLeave;
                rotationAngle = 0;
                
                return;
            }
                var mousePos = e.GetPosition(Canvas);
                pointsList.Add(mousePos);
                if (pointsList.Count == ToolNow.CountOfPoints)
                {
                    isMove = false;
                }
           
        }
        private void Canvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
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
              
                    rotationAngle += 10; 
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
                    RmvLstShp();
                    Point[] pointsArray = pointsList.ToArray();
                    pointsArray[pointsArray.Length - 1] = mousePosition;
                    CreateShapeOnCanvas(FillColorPicker.SelectedBrush, pointsArray);
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
                            RmvLstShp();
                        }
                        else
                        {
                            isMove = true;
                        }

                        Point[] pointsArray = pointsList.ToArray();
                        pointsArray[pointsArray.Length - 1] = mousePosition;
                        CreateShapeOnCanvas(FillColorPicker.SelectedBrush, pointsArray);
                        pointsList[pointsList.Count - 1] = mousePosition;
                    }
                    
                    else if (ToolNow.CountOfPoints == -1 && pointsList.Count > 1)
                    {
                        var mousePosition = e.GetPosition(Canvas);
                        if (isMove)
                        {   
                            int count = Canvas.Children.Count;
                            if (count > 0)
                            {
                                List<Point> shapePoints = new List<Point>(SpritesList[SpritesList.Count - 1].Points);
                                Point last = pointsList[pointsList.Count - 2];
                                if(shapePoints.Contains(last))
                                    RmvLstShp();
                            }
                                  
                        }
                        else
                        {
                            isMove = true;
                        }
                        Point[] pointsArray = pointsList.ToArray();
                        pointsArray[pointsArray.Length - 1] = mousePosition;
                        CreateShapeOnCanvas(FillColorPicker.SelectedBrush, pointsArray);
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
        
        void CreateShapeOnCanvas(Brush bg, Point[] points )
        {
            MySprite shape = ToolNow.Factory.CreateShape(bg, PenColorPicker.SelectedBrush,points, rotationAngle);
            
            SpritesList.Add(shape);
            DrawShapeOnCanvas(Canvas, shape);
            Canvas.Children[shape.CanvasIndex].PreviewMouseUp += Canvas_OnMouseUp;
            Canvas.Children[shape.CanvasIndex].PreviewMouseWheel += Window_MouseWheel;
            Canvas.Children[shape.CanvasIndex].MouseEnter += Shape_MouseEnter;
            Canvas.Children[shape.CanvasIndex].MouseLeave += Shape_MouseLeave;
        }
        
        void Shape_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Shape s)
            {
                
                s.StrokeThickness += 3;
            }
        }
        
        void Shape_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Shape s)
            {
               
                s.StrokeThickness -= 3;
            }
        }

        void BinarySave_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem clickedMenuItem = sender as MenuItem;
            string menuItemTag = clickedMenuItem.Tag?.ToString();
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Бинарные файлы (*.dat)|*.dat|Все файлы (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                if (!saveFileDialog.FileName.EndsWith(".dat"))
                    saveFileDialog.FileName += ".dat";
                
                if (CurrentFunctionalPlugins != null && CurrentFunctionalPlugins.Count > 0 && menuItemTag != "Default")
                {
                    List<MySpriteForEncrypt> list = new();
                    foreach (var kvp in CurrentFunctionalPlugins)
                    {
                        if (kvp.Value.Item1 && kvp.Value.Item2.Name == menuItemTag)
                        {
                            list = kvp.Value.Item2.EncryptData(SpritesList);
                            break; // Выходим из цикла, если плагин найден
                        }
                    }
                    using FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
#pragma warning disable SYSLIB0011
                    BinaryFormatter formatter = new BinaryFormatter();
#pragma warning restore SYSLIB0011
                    formatter.Serialize(fs, list);
                }
                else
                {
                    List<MySpriteForBinary> list = new();
                    foreach (var item in SpritesList)
                    {
                        list.Add(new()
                        {
                            Angle = item._rotationAngle,
                            BackgroundColor = item.FillColor.ToString(),
                            Points = item.Points,
                            PenColor = item.StrokeColor.ToString(),
                            StrokeThickness = item.StrokeThickness,
                            idOfClassShape = item.idOfClassShape,
                            countOfPoints = item.countOfPoints
                        });
                    }
                    using FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
#pragma warning disable SYSLIB0011
                    BinaryFormatter formatter = new BinaryFormatter();
#pragma warning restore SYSLIB0011
                    formatter.Serialize(fs, list);
                }
                
                
            }
        }
        
        private void OpenBinary_Click(object sender, RoutedEventArgs e)
        {
            MenuItem clickedMenuItem = sender as MenuItem;
            string menuItemTag = clickedMenuItem.Tag?.ToString();
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Бинарные файлы (*.dat)|*.dat"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    List<MySpriteForEncrypt> encryptedList = new List<MySpriteForEncrypt>();
                    if (CurrentFunctionalPlugins != null && CurrentFunctionalPlugins.Count > 0 && menuItemTag != "Default")
                    {
                        foreach (var kvp in CurrentFunctionalPlugins)
                        {
                            if (kvp.Value.Item1 && kvp.Value.Item2.Name == menuItemTag)
                            {
                                using (FileStream fsPlugin = new FileStream(openFileDialog.FileName, FileMode.Open))
                                {
                                    
#pragma warning disable SYSLIB0011
                                    BinaryFormatter formatterPlugin = new BinaryFormatter();
#pragma warning restore SYSLIB0011
                                    encryptedList = (List<MySpriteForEncrypt>)formatterPlugin.Deserialize(fsPlugin);
                                }
                            }
                        }
                    }
                    
                    if (encryptedList.Count > 0)
                    {
                       
                        List<MySprite> decryptedSprites = new List<MySprite>();
                        if (CurrentFunctionalPlugins != null && CurrentFunctionalPlugins.Count > 0 && menuItemTag != "Default")
                        {
                            foreach (var kvp in CurrentFunctionalPlugins)
                            {
                                if (kvp.Value.Item1 && kvp.Value.Item2.Name == menuItemTag)
                                {
                                    decryptedSprites = kvp.Value.Item2.DecryptData(encryptedList, ToolArr);
                                }
                            }
                        }
                        // Отображаем дешифрованные данные
                        SpritesList.Clear();
                        Canvas.Children.Clear();
                        foreach (var item in decryptedSprites)
                        {
                            // Добавляем дешифрованные фигуры на холст и в список SpritesList
                            SpritesList.Add(item);
                            ToolNow = ToolArr[item.idOfClassShape];
                            DrawShapeOnCanvas(Canvas, item);

                            Canvas.Children[item.CanvasIndex].PreviewMouseUp += Canvas_OnMouseUp;
                            Canvas.Children[item.CanvasIndex].PreviewMouseWheel += Window_MouseWheel;
                            Canvas.Children[item.CanvasIndex].MouseEnter += Shape_MouseEnter;
                            Canvas.Children[item.CanvasIndex].MouseLeave += Shape_MouseLeave;
                        }

                        MessageBox.Show($"Ваши фигуры успешно десериализованы!");
                    }
                    else
                    {
                        using FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
#pragma warning disable SYSLIB0011
                        BinaryFormatter formatter = new BinaryFormatter();
#pragma warning restore SYSLIB0011
                    
                        if (formatter.Deserialize(fs) is List<MySpriteForBinary> { Count: not 0 } loadedShapes)
                        {
                            SpritesList.Clear();
                            Canvas.Children.Clear();
                            foreach (var item in loadedShapes)
                            {
                                var shape = ToolArr[item.idOfClassShape].Factory.CreateShape(
                                    (SolidColorBrush)new BrushConverter().ConvertFromString(item.BackgroundColor), 
                                    (SolidColorBrush)new BrushConverter().ConvertFromString(item.PenColor),
                                    item.Points, 
                                    item.Angle);
                                shape.StrokeThickness = item.StrokeThickness;
                        
                                SpritesList.Add(shape);
                                ToolNow = ToolArr[item.idOfClassShape];
                                DrawShapeOnCanvas(Canvas, shape);

                                Canvas.Children[shape.CanvasIndex].PreviewMouseUp += Canvas_OnMouseUp;
                                Canvas.Children[shape.CanvasIndex].PreviewMouseWheel += Window_MouseWheel;
                                Canvas.Children[shape.CanvasIndex].MouseEnter += Shape_MouseEnter;
                                Canvas.Children[shape.CanvasIndex].MouseLeave += Shape_MouseLeave;
                            }
                    
                            MessageBox.Show($"Ваши фигуры успешно десериализованы!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии файла Binary: {ex.Message}");
                }
                
                
            }
        }
        void XMLSave_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem clickedMenuItem = sender as MenuItem;
            string menuItemTag = clickedMenuItem.Tag?.ToString();
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "XML файлы (*.xml)|*.xml|Все файлы (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                if (!saveFileDialog.FileName.EndsWith(".xml"))
                {
                    saveFileDialog.FileName += ".xml";
                }
                using FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                if (CurrentFunctionalPlugins != null && CurrentFunctionalPlugins.Count > 0 && menuItemTag != "Default")
                {
                    List<MySpriteForEncrypt> list = new();
                    
                    foreach (var kvp in CurrentFunctionalPlugins)
                    {
                        if (kvp.Value.Item1 && kvp.Value.Item2.Name == menuItemTag)
                        {
                            list = kvp.Value.Item2.EncryptData(SpritesList);
                            break; // Выходим из цикла, если плагин найден
                        }
                    }
                    
                    XmlSerializer serializer = new XmlSerializer(typeof(List<MySpriteForEncrypt>));
                    serializer.Serialize(fs, list);
                }
                else
                {
                    List<MySpriteForXML> list = new();
                    foreach (var item in SpritesList)
                    {
                        list.Add(new()
                        {
                            Angle = item._rotationAngle,
                            BackgroundColor = item.FillColor,
                            Points = item.Points,
                            PenColor = item.StrokeColor,
                            StrokeThickness = item.StrokeThickness,
                            idOfClassShape = item.idOfClassShape,
                            countOfPoints = item.countOfPoints
                        });
                    }
            
                    XmlSerializer serializer = new XmlSerializer(typeof(List<MySpriteForXML>));
                    serializer.Serialize(fs, list);
                }
                
            }
        }
        
        void XMLOpen_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem clickedMenuItem = sender as MenuItem;
            string menuItemTag = clickedMenuItem.Tag?.ToString();
             OpenFileDialog openFileDialog = new()
            {
                Filter = "XML файлы (*.xml)|*.xml"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    List<MySpriteForEncrypt> encryptedList = new List<MySpriteForEncrypt>();
                    if (CurrentFunctionalPlugins != null && CurrentFunctionalPlugins.Count > 0 && menuItemTag != "Default")
                    {
                        foreach (var kvp in CurrentFunctionalPlugins)
                        {
                            if (kvp.Value.Item1 && kvp.Value.Item2.Name == menuItemTag)
                            {
                                using (FileStream fsPlugin = new FileStream(openFileDialog.FileName, FileMode.Open))
                                {
                                    XmlSerializer serializer = new XmlSerializer(typeof(List<MySpriteForEncrypt>));
                                    encryptedList = (List<MySpriteForEncrypt>)serializer.Deserialize(fsPlugin);
                                }
                            }
                        }
                    }
                    if (encryptedList.Count > 0)
                    {
                        // Дешифруем данные с использованием плагина шифрования
                        List<MySprite> decryptedSprites = new List<MySprite>();
                        if (CurrentFunctionalPlugins != null && CurrentFunctionalPlugins.Count > 0 && menuItemTag != "Default")
                        {
                            foreach (var kvp in CurrentFunctionalPlugins)
                            {
                                if (kvp.Value.Item1 && kvp.Value.Item2.Name == menuItemTag)
                                {
                                    decryptedSprites = kvp.Value.Item2.DecryptData(encryptedList, ToolArr);
                                }
                            }
                        }
                        // Отображаем дешифрованные данные
                        SpritesList.Clear();
                        Canvas.Children.Clear();
                        foreach (var item in decryptedSprites)
                        {
                            // Добавляем дешифрованные фигуры на холст и в список SpritesList
                            SpritesList.Add(item);
                            ToolNow = ToolArr[item.idOfClassShape];
                            DrawShapeOnCanvas(Canvas, item);

                            Canvas.Children[item.CanvasIndex].PreviewMouseUp += Canvas_OnMouseUp;
                            Canvas.Children[item.CanvasIndex].PreviewMouseWheel += Window_MouseWheel;
                            Canvas.Children[item.CanvasIndex].MouseEnter += Shape_MouseEnter;
                            Canvas.Children[item.CanvasIndex].MouseLeave += Shape_MouseLeave;
                        }

                        MessageBox.Show($"Ваши фигуры успешно десериализованы!");
                    }
                    else
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<MySpriteForXML>));
                        using FileStream fs = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate);

                        if (serializer.Deserialize(fs) is List<MySpriteForXML> { Count: not 0 } loadedShapes)
                        {
                            SpritesList.Clear();
                            Canvas.Children.Clear();
                            foreach (var item in loadedShapes)
                            {
                                var shape = ToolArr[item.idOfClassShape].Factory.CreateShape(
                                    item.BackgroundColor, 
                                    item.PenColor,
                                    item.Points, 
                                    item.Angle);
                                shape.StrokeThickness = item.StrokeThickness;
                        
                                SpritesList.Add(shape);
                                ToolNow = ToolArr[item.idOfClassShape];
                                DrawShapeOnCanvas(Canvas, shape);

                                Canvas.Children[shape.CanvasIndex].PreviewMouseUp += Canvas_OnMouseUp;
                                Canvas.Children[shape.CanvasIndex].PreviewMouseWheel += Window_MouseWheel;
                                Canvas.Children[shape.CanvasIndex].MouseEnter += Shape_MouseEnter;
                                Canvas.Children[shape.CanvasIndex].MouseLeave += Shape_MouseLeave;
                            }
                    
                            MessageBox.Show($"Ваши фигуры успешно десериализованы");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии файла XML: {ex.Message}");
                }
            }
        }
        
        
        void LoadPlugin(object sender, RoutedEventArgs e)
        {
            
                PluginLoader pluginLoader = new();
                var factoryList = pluginLoader.LoadPlugin();
                foreach (var factory in factoryList)
                {
                    Point[] temp =  { new Point(0, 0), new Point(0, 0)};
                    var shape = factory.CreateShape(Brushes.Black,Brushes.Black ,temp, 0);
                    if (!ToolArr.ContainsKey(shape.idOfClassShape))
                    {
                        ToolArr.Add(shape.idOfClassShape, new ToolType(factory,shape.DrawStrategy,shape.countOfPoints));
                    }


                    Button newButton = new Button
                    {
                        Content = shape.ToString(),
                        Tag = shape.idOfClassShape,
                    };
                    newButton.Click += ToolBtnClick;
                    
                    int rowIndex = ButtonGrid.RowDefinitions.Count;
                    ButtonGrid.RowDefinitions.Add(
                        new()
                        {
                            Height = new GridLength(1, GridUnitType.Star),
                        });
                    Grid.SetRow(newButton, rowIndex);
                    ButtonGrid.Children.Add(newButton);
                    ToolNow = ToolArr[shape.idOfClassShape];
                }
        }
        
        void LoadPluginFunc(object sender, RoutedEventArgs e)
        {
                PluginLoader pluginLoader = new();
                var list = pluginLoader.LoadPluginFunctionality();
                foreach (var item in list)
                {
                    if (CurrentFunctionalPlugins.TryGetValue(item.GetType().Name, out var result))
                    {
                        MessageBox.Show($"Функциональность '{item.Name}' уже загружена.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        continue;
                    }

                    CurrentFunctionalPlugins[item.GetType().Name] = Tuple.Create(true, item);
                
                    MenuItem menuItem = new()
                    {
                        Header = item.Name,
                    };
                   
                    MenuItem saveBin = new MenuItem();
                    saveBin.Header = "Сериализовать в Bin";
                    saveBin.Click += BinarySave_OnClick;
                    saveBin.Tag = item.Name;
                    menuItem.Items.Add(saveBin);
                    
                    MenuItem SaveXML = new MenuItem();
                    SaveXML.Header = "Сериализовать в XML";
                    SaveXML.Click += XMLSave_OnClick;
                    SaveXML.Tag = item.Name;
                    menuItem.Items.Add(SaveXML);
                        
                    MenuItem OpenBin = new MenuItem();
                    OpenBin.Header = "Десериализовать из Bin";
                    OpenBin.Click += OpenBinary_Click;
                    OpenBin.Tag = item.Name;
                    menuItem.Items.Add(OpenBin);
                        
                    MenuItem OpenXML = new MenuItem();
                    OpenXML.Header = "Десериализовать из XML";
                    OpenXML.Click += XMLOpen_OnClick;
                    OpenXML.Tag = item.Name;
                    menuItem.Items.Add(OpenXML);    
                    
                    FuncPanel.Items.Add(menuItem);
                }
        }
        
        
        
        void Canvas_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && !isMove)
            {
                    if (pointsList.Count == ToolNow.CountOfPoints)
                    {
                        var mousePosition = e.GetPosition(Canvas);
                   
                        isMove = false;
                        Point[] pointsArray = pointsList.ToArray();
                        pointsArray[pointsArray.Length - 1] = mousePosition;
                        pointsList[pointsList.Count - 1] = mousePosition;
                        CreateShapeOnCanvas(FillColorPicker.SelectedBrush, pointsArray);
                        pointsList.Clear(); 
                        rotationAngle = 0;
                    }

                    if (ToolNow.CountOfPoints == -1 && pointsList.Count > 1)
                    {
                        var mousePosition = e.GetPosition(Canvas);
                        int count = Canvas.Children.Count; 
                        if (count > 0 && pointsList.Count > 2)
                            Canvas.Children.RemoveAt(count - 1);
                        isMove = false;
                        Point[] pointsArray = pointsList.ToArray();
                        pointsArray[pointsArray.Length - 1] = mousePosition;
                        pointsList[pointsList.Count - 1] = mousePosition;
                        CreateShapeOnCanvas(FillColorPicker.SelectedBrush, pointsArray);
                        rotationAngle = 0;
                    }
   
            }
        }
}