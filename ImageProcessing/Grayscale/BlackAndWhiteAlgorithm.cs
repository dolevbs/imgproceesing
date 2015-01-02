using System.Drawing;

namespace Grayscale
{
    public class BlackAndWhiteAlgorithm : UseGrayScaleAlgorithm, IBitmapSource
    {
        private Bitmap _blackAndWhiteBitmap;

        public BlackAndWhiteAlgorithm(GrayScaleAlgorithm bitmapSource)
            : base(bitmapSource)
        {
        }

        public override Bitmap Execute()
        {
            //make an empty bitmap the same size as original
            var newBitmap = new Bitmap(BitmapSource.Bitmap.Width, BitmapSource.Bitmap.Height);

            for (int i = 0; i < BitmapSource.Bitmap.Width; i++)
            {
                for (int j = 0; j < BitmapSource.Bitmap.Height; j++)
                {
                    //get the pixel from the original image
                    var originalColor = BitmapSource.Bitmap.GetPixel(i, j);

                    var scale = originalColor.R < 90 ? 0 : 255;

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, Color.FromArgb(scale, scale, scale));
                }
            }

            _blackAndWhiteBitmap = newBitmap;

            return newBitmap;
        }

        public Bitmap Bitmap
        {
            get { return _blackAndWhiteBitmap ?? Execute(); }
        }
    }
}