using System.Drawing;

namespace Grayscale.AlgorithmsBase
{
    public class SimpleBitmapSource : IBitmapSource
    {
        public SimpleBitmapSource(Bitmap bitmap)
        {
            Bitmap = bitmap;
        }

        public Bitmap Bitmap { get; private set; }
    }
}