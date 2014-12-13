using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Color = System.Drawing.Color;

namespace Grayscale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Bitmap _grayScaleBitmap;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Browse_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Open Image",
                Filter = "Image Files (*.jpg,*.jpeg,*.png)|*.jpg;*.jpeg;*.png",
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

            _grayScaleBitmap = MakeGrayscale(new Bitmap(openFileDialog.FileName));

            OriginalImage.Source = BitmapUtils.ConvertBitmap(_grayScaleBitmap);
        }

        public Bitmap MakeGrayscale(Bitmap original)
        {
            //make an empty bitmap the same size as original
            var newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    var originalColor = original.GetPixel(i, j);

                    //create the grayscale version of the pixel
                    var grayScale = (byte) ((originalColor.R*.3) + (originalColor.G*.59) + (originalColor.B*.11));

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, Color.FromArgb(grayScale, grayScale, grayScale));
                }
            }

            return newBitmap;
        }

        private void HistogramEqualization_Click(object sender, RoutedEventArgs e)
        {
            var newBitmap = MakeHistogramEqualization();

            OutputImage.Source = BitmapUtils.ConvertBitmap(newBitmap);
        }

        private Bitmap MakeHistogramEqualization()
        {
            // calculate the frequency of the image
            var frequencyOfImageAr = new int[256];
            var grayscaledBitmap = _grayScaleBitmap;

            for (var i = 0; i < grayscaledBitmap.Width; i++)
            {
                for (var j = 0; j < grayscaledBitmap.Height; j++)
                {
                    //get the pixel from the original image
                    var originalColor = grayscaledBitmap.GetPixel(i, j);
                    frequencyOfImageAr[originalColor.R]++;
                }
            }

            // calculate the cuf function
            var cdfArray = new int[256];
            int numberOfPixelsInImage = grayscaledBitmap.Width*grayscaledBitmap.Height;
            int cufCounter = numberOfPixelsInImage;
            int cdfMinValue = numberOfPixelsInImage;

            for (int i = 255; i >= 0; i--)
            {
                cdfArray[i] = cufCounter - frequencyOfImageAr[i];
                cufCounter -= frequencyOfImageAr[i];
                if (cdfArray[i] != 0 && cdfArray[i] < cdfMinValue)
                {
                    cdfMinValue = cdfArray[i];
                }
            }

            var newBitmap = new Bitmap(_grayScaleBitmap.Width, _grayScaleBitmap.Height);
            for (int i = 0; i < newBitmap.Width; i++)
            {
                for (int j = 0; j < newBitmap.Height; j++)
                {
                    //get the pixel from the original image
                    var originalColor = grayscaledBitmap.GetPixel(i, j);

                    //create the grayscale version of the pixel
                    //int hi = (cdfAr[originalColor.R] - cdfMinValue) / (numberOfPixelsInImage)
                    var hi =
                        Math.Round((cdfArray[originalColor.R] - cdfMinValue)/
                                   (double) (numberOfPixelsInImage - cdfMinValue)*255);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, Color.FromArgb((byte) hi, (byte) hi, (byte) hi));
                }
            }
            return newBitmap;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            var inputFilterDialog = new InputFilterDialog();

            var dialogResult = inputFilterDialog.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                return;
            }

            var targetCell = inputFilterDialog.TargetCellIndex;
            var selectedSize = inputFilterDialog.SelectedSize;
            var filterMatrix = inputFilterDialog.FilterMatrix;
        }
    }

    public class BitmapUtils
    {
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        public static BitmapSource ConvertBitmap(Bitmap source)
        {
            IntPtr ip = source.GetHbitmap();
            BitmapSource bs;
            try
            {
                bs = Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(ip);
            }

            return bs;
        }
    }
}