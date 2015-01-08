using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Grayscale.Algorithms;
using Grayscale.AlgorithmsBase;
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
        private BlackAndWhiteAlgorithm _blackAndWhiteAlgorithm;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Browse_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Open Image",
                Filter = "Image Files (*.jpg,*.jpeg,*.png,*.gif)|*.jpg;*.jpeg;*.png;*.gif",
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
            _blackAndWhiteAlgorithm = new BlackAndWhiteAlgorithm(_grayScaleAlgorithm);

            OriginalImage.Source = BitmapUtils.ConvertBitmap(bitmap);
        }

        private void HistogramEqualization_Click(object sender, RoutedEventArgs e)
        {
            var algorithem = new HistogramEqualization(_grayScaleAlgorithm);

            ExecuteAndSetImages(algorithem);
        }

        private void ExecuteAndSetImages(BitmapAlgorithm algorithem)
        {
            OriginalImage.Source = BitmapUtils.ConvertBitmap(algorithem.BitmapSource.Result);
            OutputImage.Source = BitmapUtils.ConvertBitmap(algorithem.Result);
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

            var filterAlgorithm = new FilterAlgorithm(_grayScaleAlgorithm, filterMatrix, targetCell);

            ExecuteAndSetImages(filterAlgorithm);
        }

        private void BlackAndWhite_Click(object sender, RoutedEventArgs e)
        {
            ExecuteAndSetImages(_blackAndWhiteAlgorithm);
        }

        private void GrayScale_Click(object sender, RoutedEventArgs e)
        {
            ExecuteAndSetImages(_grayScaleAlgorithm);
        }

        private static bool GetStructuringElement(out bool?[,] structuringElement, out Point target)
        {
            var dialog = new InputStructuringElementDialog();

            var dialogResult = dialog.ShowDialog();

            structuringElement = dialog.Result;
            target = dialog.Target;

            if (!dialogResult.HasValue || !dialogResult.Value)
            {
                return false;
            }

            return true;
        }

        private void ErosionClick(object sender, RoutedEventArgs e)
        {
            Point target;
            bool?[,] structuringElement;

            bool confirmed = GetStructuringElement(out structuringElement, out target);

            if (!confirmed)
            {
                return;
            }

            ExecuteAndSetImages(new ErosionAlgorithm(_blackAndWhiteAlgorithm, structuringElement, target));
        }

        private void DilationClick(object sender, RoutedEventArgs e)
        {
            Point target;
            bool?[,] structuringElement;

            bool confirmed = GetStructuringElement(out structuringElement, out target);

            if (!confirmed)
            {
                return;
            }

            ExecuteAndSetImages(new DilationAlgorithm(_blackAndWhiteAlgorithm, structuringElement, target));
        }

        private void OpeningClick(object sender, RoutedEventArgs e)
        {
            Point target;
            bool?[,] structuringElement;

            bool confirmed = GetStructuringElement(out structuringElement, out target);

            if (!confirmed)
            {
                return;
            }

            var erosionAlgorithm = new ErosionAlgorithm(_blackAndWhiteAlgorithm, structuringElement, target);
            var dilationAlgorithm = new DilationAlgorithm(erosionAlgorithm, structuringElement, target);

            ExecuteAndSetImages(dilationAlgorithm);
        }

        private void ClosingClick(object sender, RoutedEventArgs e)
        {
            Point target;
            bool?[,] structuringElement;

            bool confirmed = GetStructuringElement(out structuringElement, out target);

            if (!confirmed)
            {
                return;
            }

            var dilationAlgorithm = new DilationAlgorithm(_blackAndWhiteAlgorithm, structuringElement, target);
            var erosionAlgorithm = new ErosionAlgorithm(dilationAlgorithm, structuringElement, target);

            ExecuteAndSetImages(erosionAlgorithm);
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