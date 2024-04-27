using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Media;
using components;

namespace EncryptPlugin
{
    
    
    
    
    public class ChipherPlugin : IPluginFunctionality

    {
        public string Name => "Шифровать при сохранении";

        // Ключ шифрования и вектор инициализации (IV)
        private static readonly byte[] EncryptionKey = Encoding.UTF8.GetBytes("12345678901234561234567890123456");
        private static readonly byte[] EncryptionIV = Encoding.UTF8.GetBytes("1234567890123456");

        public List<MySpriteForEncrypt> EncryptData(List<MySprite> data)
        {
            List<MySpriteForEncrypt> encryptedData = new List<MySpriteForEncrypt>();
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = EncryptionKey;
                aesAlg.IV = EncryptionIV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                foreach (var item in data)
                {
                    MySpriteForEncrypt encryptedItem = ConvertToMySpriteForEncrypt(item);
                    encryptedData.Add(EncryptItemFields(encryptedItem, encryptor));
                }
            }

            return encryptedData;
        }

        public List<MySprite> DecryptData(List<MySpriteForEncrypt> encryptedData, Dictionary<object, ToolType> ToolArr)
        {
            List<MySprite> decryptedData = new List<MySprite>();
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = EncryptionKey;
                aesAlg.IV = EncryptionIV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                foreach (var item in encryptedData)
                {
                    MySpriteForEncrypt decryptedItem = DecryptItemFields(item, decryptor);
                    MySprite sprite = ConvertToMySprite(decryptedItem, ToolArr);
                    if (sprite != null)
                    {
                        decryptedData.Add(sprite);
                    }
                }
            }

            return decryptedData;
        }

        private MySpriteForEncrypt EncryptItemFields(MySpriteForEncrypt item, ICryptoTransform encryptor)
        {
            MySpriteForEncrypt encryptedItem = new MySpriteForEncrypt
            {
                idOfClassShape = EncryptString(item.idOfClassShape, encryptor),
                Angle = EncryptString(item.Angle, encryptor),
                Points = EncryptString(item.Points, encryptor),
                StrokeThickness = EncryptString(item.StrokeThickness, encryptor),
                BackgroundColor = EncryptString(item.BackgroundColor, encryptor),
                PenColor = EncryptString(item.PenColor, encryptor)
            };
            return encryptedItem;
        }

        private MySpriteForEncrypt DecryptItemFields(MySpriteForEncrypt item, ICryptoTransform decryptor)
        {
            MySpriteForEncrypt decryptedItem = new MySpriteForEncrypt
            {
                idOfClassShape = DecryptString(item.idOfClassShape, decryptor),
                Angle = DecryptString(item.Angle, decryptor),
                Points = DecryptString(item.Points, decryptor),
                StrokeThickness = DecryptString(item.StrokeThickness, decryptor),
                BackgroundColor = DecryptString(item.BackgroundColor, decryptor),
                PenColor = DecryptString(item.PenColor, decryptor)
            };
            return decryptedItem;
        }

        private string EncryptString(string plainText, ICryptoTransform encryptor)
        {
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }

        private string DecryptString(string cipherText, ICryptoTransform decryptor)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        public static MySpriteForEncrypt ConvertToMySpriteForEncrypt(MySprite sprite)
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

        public static MySprite ConvertToMySprite(MySpriteForEncrypt sprite, Dictionary<object, ToolType> ToolArr)
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
        public static Point[] StringToPoints(string pointsString)
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
    
    
}