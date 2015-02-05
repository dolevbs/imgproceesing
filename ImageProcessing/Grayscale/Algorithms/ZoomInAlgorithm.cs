using System;
using System.Drawing;
using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class ZoomInAlgorithm : BitmapAlgorithm
    {
        private readonly double _widthFactor;
        private readonly double _heightFactor;

        public ZoomInAlgorithm(IBitmapSource bitmapSource, double widthFactor, double heightFactor) : base(bitmapSource)
        {
            _widthFactor = widthFactor;
            _heightFactor = heightFactor;
        }

        protected override Bitmap Execute()
        {
            throw new NotImplementedException();
        }
    }
}