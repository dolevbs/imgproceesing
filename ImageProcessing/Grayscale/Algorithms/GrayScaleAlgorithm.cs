using System.Drawing;
using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class GrayScaleAlgorithm : BitmapAlgorithm, IGrayScaleBitmapSource
    {
        public GrayScaleAlgorithm(IBitmapSource bitmapSource)
            : base(bitmapSource)
        {
        }

        protected override Bitmap Execute()
        {
            Bitmap bitmap = BitmapSource.Result;

            var newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    //get the pixel from the original image
                    var originalColor = bitmap.GetPixel(i, j);

                    //create the grayscale version of the pixel
                    var grayScale = (byte) ((originalColor.R*.3) + (originalColor.G*.59) + (originalColor.B*.11));

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, Color.FromArgb(grayScale, grayScale, grayScale));
                }
            }

            return newBitmap;
        }
    }
}