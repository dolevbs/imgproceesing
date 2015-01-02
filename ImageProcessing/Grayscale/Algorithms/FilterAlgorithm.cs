using System.Drawing;
using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class FilterAlgorithm : UseGrayScaleAlgorithm
    {
        private readonly double[,] _filterMatrix;
        private readonly Point _target;

        public FilterAlgorithm(GrayScaleAlgorithm grayScaleBitmap, double[,] filterMatrix, Point target)
            : base(grayScaleBitmap)
        {
            _filterMatrix = filterMatrix;
            _target = target;
        }

        public override void Execute()
        {
            var filterSize = _filterMatrix.GetLength(0);

            Bitmap grayScaleBitmap = BitmapSource.Bitmap;

            var newBitmap = new Bitmap(grayScaleBitmap.Width - filterSize, grayScaleBitmap.Height - filterSize);

            var startI = _target.X;
            var startJ = _target.Y;
            var right = filterSize - _target.X;
            var down = filterSize - _target.Y;

            for (int i = startI; i < grayScaleBitmap.Width - right; i++)
            {
                for (int j = startJ; j < grayScaleBitmap.Height - down; j++)
                {
                    var filterd = CalculateFilterOnPoint(new Point(i - startI, j - startJ), _filterMatrix, grayScaleBitmap);
                    newBitmap.SetPixel(i - startI, j - startJ, Color.FromArgb(filterd, filterd, filterd));
                }
            }

            Result = newBitmap;
        }

        private byte CalculateFilterOnPoint(Point target, double[,] filterMatrix, Bitmap grayScaleBitmap)
        {
            double calculatedValue = 0;
            var grayscaledBitmap = grayScaleBitmap;

            for (var i = 0; i < filterMatrix.GetLength(0); i++)
            {
                for (var j = 0; j < filterMatrix.GetLength(1); j++)
                {
                    var originalColor = grayscaledBitmap.GetPixel(target.X + i, target.Y + j);
                    calculatedValue += originalColor.R*filterMatrix[i, j];
                }
            }

            return RoundAndClampColor(calculatedValue);
        }
    }
}