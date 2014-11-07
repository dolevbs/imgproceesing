using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Color = System.Windows.Media.Color;

namespace Grayscale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Browse_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Open Image",
                Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg",
                Multiselect = false
            };

            var showDialog = openFileDialog.ShowDialog();
            if (!showDialog.HasValue || !showDialog.Value)
            {
                return;
            }


            if (!File.Exists(openFileDialog.FileName))
            {
                MessageBox.Show(this, "File Dosn't Exist, can't open");
                return;
            }

            var src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(openFileDialog.FileName, UriKind.Absolute);
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();

            OriginalImage.Source = src;

        }

        private void Grayscale_OnClick(object sender, RoutedEventArgs e)
        {
            GrayscaleImage.Source = MakeGrayscale();
        }

        public ImageSource MakeGrayscale()
        {
            var original = BitmapFactory.ConvertToPbgra32Format(OriginalImage.Source as BitmapSource);

            //make an empty bitmap the same size as original
            var newBitmap = new WriteableBitmap((BitmapSource)OriginalImage.Source);

            for (int i = 0; i < original.PixelWidth; i++)
            {
                for (int j = 0; j < original.PixelHeight; j++)
                {
                    //get the pixel from the original image
                    var originalColor = original.GetPixel(i, j);

                    //create the grayscale version of the pixel
                    var grayScale = (byte) ((originalColor.R*.3) + (originalColor.G*.59) + (originalColor.B*.11));

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, Color.FromRgb(grayScale, grayScale, grayScale));
                }
            }

            return newBitmap;
        }
    }
}