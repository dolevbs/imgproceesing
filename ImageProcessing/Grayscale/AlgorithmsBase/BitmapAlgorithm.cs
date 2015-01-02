using System.Drawing;

namespace Grayscale.AlgorithmsBase
{
    public abstract class BitmapAlgorithm
    {
        protected BitmapAlgorithm(IBitmapSource bitmapSource)
        {
            BitmapSource = bitmapSource;
        }

        public IBitmapSource BitmapSource { get; private set; }

        public abstract void Execute();

        public Bitmap Result { get; protected set; }
    }
}