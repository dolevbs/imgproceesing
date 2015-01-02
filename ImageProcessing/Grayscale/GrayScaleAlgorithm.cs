using System.Drawing;

namespace Grayscale
{
    public class GrayScaleAlgorithm : BitmapAlgorithm, IBitmapSource
    {
        private Bitmap _grayScaledBitmap;

        public GrayScaleAlgorithm(IBitmapSource bitmapSource)
            : base(bitmapSource)
        {
        }

        public override Bitmap Execute()
        {
            Bitmap bitmap = BitmapSource.Bitmap;

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

            _grayScaledBitmap = newBitmap;

            return newBitmap;
        }

        public Bitmap Bitmap
        {
            get
            {
                return _grayScaledBitmap ?? Execute();
            }
        }
    }
}