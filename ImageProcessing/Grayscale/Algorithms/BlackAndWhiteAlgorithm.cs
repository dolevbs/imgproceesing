using System.Drawing;
using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class BlackAndWhiteAlgorithm : UseGrayScaleAlgorithm, IBlackAndWhiteBitmapSource
    {
        public BlackAndWhiteAlgorithm(IGrayScaleBitmapSource bitmapSource)
            : base(bitmapSource)
        {
        }

        protected override Bitmap Execute()
        {
            //make an empty bitmap the same size as original
            Bitmap bitmap = BitmapSource.Result;

            var newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    //get the pixel from the original image
                    var originalColor = bitmap.GetPixel(i, j);

                    var scale = originalColor.R < 90 ? 0 : 255;

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, Color.FromArgb(scale, scale, scale));
                }
            }

            return newBitmap;
        }
    }
}