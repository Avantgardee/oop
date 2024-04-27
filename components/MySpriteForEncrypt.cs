using System.Windows;
using System.Windows.Media;
using components;
using System.Windows;
namespace components;
[Serializable]
    public class MySpriteForEncrypt
    {
        public string idOfClassShape { get; set; }

        public string Angle { get; set; }

        public string Points { get; set; } // Представляем точки в виде строки

        public string StrokeThickness { get; set; }

        public string BackgroundColor { get; set; }

        public string PenColor { get; set; }
        
    }
