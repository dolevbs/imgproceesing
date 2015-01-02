using System.Drawing;

namespace Grayscale
{
    public class HistogramEqualization : UseGrayScaleAlgorithm
    {
        public HistogramEqualization(GrayScaleAlgorithm grayScaleBitmap)
            : base(grayScaleBitmap)
        {
        }

        public override Bitmap Execute()
        {
            // calculate the frequency of the image
            var frequencyOfImageAr = new int[256];

            Bitmap bitmap = BitmapSource.Bitmap;

            for (var i = 0; i < bitmap.Width; i++)
            {
                for (var j = 0; j < bitmap.Height; j++)
                {
                    //get the pixel from the original image
                    var originalColor = bitmap.GetPixel(i, j);
                    frequencyOfImageAr[originalColor.R]++;
                }
            }

            // calculate the cuf function
            var cdfArray = new int[256];
            int numberOfPixelsInImage = bitmap.Width*bitmap.Height;
            int cufCounter = numberOfPixelsInImage;
            int cdfMinValue = numberOfPixelsInImage;

            for (int i = 255; i >= 0; i--)
            {
                cdfArray[i] = cufCounter - frequencyOfImageAr[i];
                cufCounter -= frequencyOfImageAr[i];
                if (cdfArray[i] != 0 && cdfArray[i] < cdfMinValue)
                {
                    cdfMinValue = cdfArray[i];
                }
            }

            var newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            for (int i = 0; i < newBitmap.Width; i++)
            {
                for (int j = 0; j < newBitmap.Height; j++)
                {
                    //get the pixel from the original image
                    var originalColor = bitmap.GetPixel(i, j);

                    //create the grayscale version of the pixel
                    //int hi = (cdfAr[originalColor.R] - cdfMinValue) / (numberOfPixelsInImage)
                    var hi = RoundAndClampColor((cdfArray[originalColor.R] - cdfMinValue)/
                                                (double) (numberOfPixelsInImage - cdfMinValue)*255d);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, Color.FromArgb(hi, hi, hi));
                }
            }
            return newBitmap;
        }
    }
}