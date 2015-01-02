using System;
using System.Drawing;

namespace Grayscale
{
    public interface IBitmapSource
    {
        Bitmap Bitmap { get; }
    }

    public class SimpleBitmapSource : IBitmapSource
    {
        public SimpleBitmapSource(Bitmap bitmap)
        {
            Bitmap = bitmap;
        }

        public Bitmap Bitmap { get; private set; }
    }

    public abstract class BitmapAlgorithm
    {
        protected BitmapAlgorithm(IBitmapSource bitmapSource)
        {
            BitmapSource = bitmapSource;
        }

        public IBitmapSource BitmapSource { get; private set; }

        public abstract Bitmap Execute();
    }

    public abstract class UseGrayScaleAlgorithm : BitmapAlgorithm
    {
        protected UseGrayScaleAlgorithm(GrayScaleAlgorithm bitmapSource)
            : base(bitmapSource)
        {
        }

        protected static byte RoundAndClampColor(double value)
        {
            var rounded = Math.Round(value);
            if (rounded > 255)
            {
                rounded = 255;
            }
            else if (rounded < 0)
            {
                rounded = 0;
            }
            return (byte) rounded;
        }
    }
}