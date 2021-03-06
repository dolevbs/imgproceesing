using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class ZoomOutAlgorithm : BitmapAlgorithm
    {
        private readonly double _heightMultiplier;
        private readonly double _widthMultiplier;

        public ZoomOutAlgorithm(IBitmapSource bitmapSource, double widthMultiplier, double heightMultiplier)
            : base(bitmapSource)
        {
            _widthMultiplier = widthMultiplier;
            _heightMultiplier = heightMultiplier;
        }

        protected override Bitmap Execute()
        {
            Bitmap src = BitmapSource.Result;

            int outputWidth = (int)Math.Round(src.Width * _widthMultiplier);
            int outputHeight = (int)Math.Round(src.Height * _heightMultiplier);

            Bitmap b = new Bitmap(outputWidth, outputHeight);

            using (Graphics g = Graphics.FromImage((Image)b))
            {
                g.DrawImage(src, 0, 0, outputWidth, outputHeight);
            }

            return b;
        }
    }
}