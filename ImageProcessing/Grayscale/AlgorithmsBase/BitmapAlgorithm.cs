using System.Drawing;

namespace Grayscale.AlgorithmsBase
{
    public abstract class BitmapAlgorithm : IBitmapSource
    {
        private Bitmap _result;

        protected BitmapAlgorithm(IBitmapSource bitmapSource)
        {
            BitmapSource = bitmapSource;
        }

        public IBitmapSource BitmapSource { get; private set; }

        protected abstract Bitmap Execute();

        public Bitmap Result
        {
            get
            {
                return _result ?? (_result = Execute());
            }
        }
    }
}