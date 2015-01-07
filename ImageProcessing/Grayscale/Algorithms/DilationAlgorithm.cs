using System.Drawing;
using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class DilationAlgorithm : UseBlackAndWhiteAlgorithm
    {
        private readonly bool?[,] _structuringElement;
        private readonly Point _target;

        public DilationAlgorithm(BlackAndWhiteAlgorithm bitmapSource, bool?[,] structuringElement, Point target)
            : base(bitmapSource)
        {
            _structuringElement = structuringElement;
            _target = target;
        }

        public override void Execute()
        {
            Bitmap bitmap = BitmapSource.Bitmap;

            var newBitmap = new Bitmap(BitmapSource.Bitmap.Width - _structuringElement.GetLength(0) + 1, BitmapSource.Bitmap.Height - _structuringElement.GetLength(1) + 1);
            int outputI = 0;
            int outputJ = 0;
            for (int i = _target.X; i < (BitmapSource.Bitmap.Width - _structuringElement.GetLength(0) + 1 + _target.X); i++, outputI++)
            {
                outputJ = 0;
                for (int j = _target.Y; j < (BitmapSource.Bitmap.Height - _structuringElement.GetLength(1) + 1 + _target.Y); j++, outputJ++)
                {
                    bool isFitting = isFitToStructure(i, j);
                    byte curColor = 0;
                    if (isFitting)
                    {
                        curColor = 255;
                    }

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(outputI, outputJ, Color.FromArgb(curColor, curColor, curColor));
                }
            }

            Result = newBitmap;
        }

        private bool isFitToStructure(int curI, int curj)
        {
            Point imgBase = new Point(curI - _target.X, curj - _target.Y);
            for (int i = 0; i < _structuringElement.GetLength(0); i++)
            {
                for (int j = 0; j < _structuringElement.GetLength(1); j++)
                {
                    // Continue on don't care
                    if (null == _structuringElement[i, j])
                    {
                        continue;
                    }

                    var curPixel = BitmapSource.Bitmap.GetPixel(imgBase.X + i, imgBase.Y + j);
                    bool curPixelBool = (255 == curPixel.R);
                    if (curPixelBool == _structuringElement[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}