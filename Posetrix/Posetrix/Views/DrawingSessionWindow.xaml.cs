using Posetrix.Converters;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System.Windows;
using System.Windows.Media.Imaging;


namespace Posetrix.Views
{
    /// <summary>
    /// Interaction logic for DrawingSessionWindow.xaml
    /// </summary>
    public partial class DrawingSessionWindow : Window
    {
        public DrawingSessionWindow()
        {
            InitializeComponent();
            DisplayWebPImage("Images/4.webp");
            //var bitmap = new BitmapImage(new Uri("pack://application:,,,/Images/undraw_workout_gcgu.png")); 
            //MyImage.Source = bitmap;
        }

        private void DisplayWebPImage(string imagePath)
        {
            using var image = Image.Load<Rgba32>(imagePath);
            // Convert ImageSharp image to BitmapSource
            var bitmapSource = ImageLoader.ConvertToBitmapSource(image);
            MyImage.Source = bitmapSource;
        }
    }
}
