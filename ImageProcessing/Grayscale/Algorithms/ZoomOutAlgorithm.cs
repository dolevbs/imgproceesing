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
            throw new NotImplementedException();
        }
    }
}