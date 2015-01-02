using System;
using Grayscale.Algorithms;

namespace Grayscale.AlgorithmsBase
{
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