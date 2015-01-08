using System.Drawing;

namespace Grayscale.AlgorithmsBase
{
    public class SimpleBitmapSource : IBitmapSource
    {
        public SimpleBitmapSource(Bitmap bitmap)
        {
            Result = bitmap;
        }

        public Bitmap Result { get; private set; }
    }
}