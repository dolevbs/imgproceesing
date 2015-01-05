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

            var newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

           
        }
    }
}