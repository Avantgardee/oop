using System.Text;
using System.Windows;
using System.Windows.Media;

namespace components;

public class Convertor
{
    public MySpriteForEncrypt ConvertToMySpriteForEncrypt(MySprite sprite)
        {
            StringBuilder pointsString = new StringBuilder();
            foreach (var point in sprite.Points)
            {
                pointsString.Append($"{point.X};{point.Y}-");
            }

            pointsString.Remove(pointsString.Length - 1, 1); // Удаляем последний символ '-'

            return new MySpriteForEncrypt
            {
                Angle = sprite._rotationAngle.ToString(),
                BackgroundColor = sprite.FillColor.ToString(),
                Points = pointsString.ToString(),
                PenColor = sprite.StrokeColor.ToString(),
                StrokeThickness = sprite.StrokeThickness.ToString(),
                idOfClassShape = sprite.idOfClassShape.ToString()
            };
        }

        public MySprite ConvertToMySprite(MySpriteForEncrypt sprite, Dictionary<object, ToolType> ToolArr)
        {
            Point[] points = StringToPoints(sprite.Points);

            if (ToolArr.TryGetValue(sprite.idOfClassShape, out var tool))
            {
                var shape = tool.Factory.CreateShape(
                    (SolidColorBrush)new BrushConverter().ConvertFromString(sprite.BackgroundColor),
                    (SolidColorBrush)new BrushConverter().ConvertFromString(sprite.PenColor),
                    points,
                    double.Parse(sprite.Angle)
                );
                shape.StrokeThickness = double.Parse(sprite.StrokeThickness);
                shape.idOfClassShape = sprite.idOfClassShape;
                return shape;
            }

            return null;
        }

// Метод для преобразования строки в массив точек
        public Point[] StringToPoints(string pointsString)
        {
            List<Point> points = new List<Point>();
            string[] pointStrings = pointsString.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var pointString in pointStrings)
            {
                string[] coordinates =
                    pointString.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (coordinates.Length == 2 && double.TryParse(coordinates[0], out double x) &&
                    double.TryParse(coordinates[1], out double y))
                {
                    points.Add(new Point(x, y));
                }
            }

            return points.ToArray();
        }
}