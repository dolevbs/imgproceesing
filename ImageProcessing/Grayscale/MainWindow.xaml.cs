using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Point = System.Drawing.Point;

namespace Grayscale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private GrayScaleAlgorithm _grayScaleAlgorithm;

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

            var bitmap = new Bitmap(openFileDialog.FileName);

            var simpleBitmapSource = new SimpleBitmapSource(bitmap);

            _grayScaleAlgorithm = new GrayScaleAlgorithm(simpleBitmapSource);

            OriginalImage.Source = BitmapUtils.ConvertBitmap(_grayScaleAlgorithm.Bitmap);
        }

        private void HistogramEqualization_Click(object sender, RoutedEventArgs e)
        {
            var newBitmap = new HistogramEqualization(_grayScaleAlgorithm).Execute();

            OutputImage.Source = BitmapUtils.ConvertBitmap(newBitmap);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            var inputFilterDialog = new InputFilterDialog();

            var dialogResult = inputFilterDialog.ShowDialog();

            if (!dialogResult.HasValue || !dialogResult.Value)
            {
                return;
            }

            Point targetCell = inputFilterDialog.TargetCellIndex;
            double[,] filterMatrix = inputFilterDialog.FilterMatrix;

            if (null == filterMatrix)
            {
                return;
            }

            Bitmap newBitmap = new FilterAlgorithm(_grayScaleAlgorithm, filterMatrix, targetCell).Execute();

            OutputImage.Source = BitmapUtils.ConvertBitmap(newBitmap);
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