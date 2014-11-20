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
        Boolean isGrayscaled;
        int[] frequencyOfImageAr;
        int[] cdfAr;        
        public MainWindow()
        {
            isGrayscaled = false;
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
            isGrayscaled = false;
        }

        private void Grayscale_OnClick(object sender, RoutedEventArgs e)
        {
            OutputImage.Source = MakeGrayscale();
            isGrayscaled = true;
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


        private void HistogramEqualizationButton_Click(object sender, RoutedEventArgs e)
        {
            if (isGrayscaled == false)
            {                
                OutputImage.Source = MakeGrayscale();
                isGrayscaled = true;
            }

            // calculate the frequency of the image
            
            frequencyOfImageAr = new int[256];           
            var grayscaledBitmap = BitmapFactory.ConvertToPbgra32Format(OutputImage.Source as BitmapSource);
            for (int i = 0; i < grayscaledBitmap.PixelWidth; i++)
            {
                for (int j = 0; j < grayscaledBitmap.PixelHeight; j++)
                {
                    //get the pixel from the original image
                    var originalColor = grayscaledBitmap.GetPixel(i, j);
                    frequencyOfImageAr[originalColor.R]++;
                }
            }
                
            // calculate the cuf function
            cdfAr = new int[256];
            int numberOfPixelsInImage = grayscaledBitmap.PixelWidth * grayscaledBitmap.PixelHeight;
            int cufCounter = numberOfPixelsInImage;
            int cdfMinValue = numberOfPixelsInImage;
            for (int i = 255; i >= 0; i--)
            {
                cdfAr[i] = cufCounter - frequencyOfImageAr[i];
                cufCounter -= frequencyOfImageAr[i];
                if (cdfAr[i] != 0 && cdfAr[i] < cdfMinValue)
                {
                    cdfMinValue = cdfAr[i];
                }
            }

            var newBitmap = new WriteableBitmap((BitmapSource)OutputImage.Source);
            for (int i = 0; i < newBitmap.PixelWidth; i++)
            {
                for (int j = 0; j < newBitmap.PixelHeight; j++)
                {
                    //get the pixel from the original image
                    var originalColor = grayscaledBitmap.GetPixel(i, j);

                    //create the grayscale version of the pixel
                    //int hi = (cdfAr[originalColor.R] - cdfMinValue) / (numberOfPixelsInImage)
                    var hi = Math.Round((double)((double)(cdfAr[originalColor.R] - cdfMinValue) / (double)(numberOfPixelsInImage - cdfMinValue)) * 255);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, Color.FromRgb((byte)hi, (byte)hi, (byte)hi));
                }
            }
            isGrayscaled = false;
            OutputImage.Source = newBitmap;
        }
    }
}