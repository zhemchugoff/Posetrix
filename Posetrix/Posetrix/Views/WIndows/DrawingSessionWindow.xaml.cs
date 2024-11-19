using Posetrix.Converters;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;


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
            //DisplayWebPImage("Images/4.webp");
            //var bitmap = new bitmapimage(new uri("pack://application:,,,/images/undraw_workout_gcgu.png"));
            //MyImage.source = bitmap;
            ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/images/undraw_workout_gcgu.png"));
            MyImage.Source = imageSource;
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
