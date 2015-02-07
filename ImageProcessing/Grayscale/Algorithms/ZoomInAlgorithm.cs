using System;
using System.Drawing;
using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class ZoomInAlgorithm : BitmapAlgorithm
    {
        private readonly double _widthMultiplier;
        private readonly double _heightMultiplier;

        public ZoomInAlgorithm(IBitmapSource bitmapSource, double widthFactor, double heightFactor) : base(bitmapSource)
        {
            _widthMultiplier = widthFactor;
            _heightMultiplier = heightFactor;
        }

        protected override Bitmap Execute()
        {   
            Bitmap src = BitmapSource.Result;

            int outputWidth = (int) Math.Round(src.Width * _widthMultiplier);
            int outputHeight = (int) Math.Round(src.Height * _heightMultiplier);
            
            Bitmap b = new Bitmap( outputWidth, outputHeight ) ;
            
            using(Graphics g = Graphics.FromImage( (Image ) b ))
            {
                g.DrawImage(src, 0, 0, outputWidth, outputHeight);
            }

            return b ;
        }
    }
}