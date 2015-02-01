using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class RotateAlgorithm : BitmapAlgorithm
    {
        private readonly double _degrees;

        public RotateAlgorithm(IBitmapSource bitmapSource, double degrees)
            : base(bitmapSource)
        {
            _degrees = degrees;
        }

        protected override Bitmap Execute()
        {
            Bitmap sourceBitmap = BitmapSource.Result;

            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            var pixelBuffer = new byte[sourceData.Stride*
                                       sourceData.Height];


            var resultBuffer = new byte[sourceData.Stride*sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);


            sourceBitmap.UnlockBits(sourceData);

            //Convert to Radians 
            var degrees = _degrees*Math.PI/180.0;

            //Calculate Offset in order to rotate on image middle 
            var xOffset = (int) (sourceBitmap.Width/2.0);
            var yOffset = (int) (sourceBitmap.Height/2.0);


            var sourcePoint = new Point();


            var imageBounds = new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height);


            for (int row = 0; row < sourceBitmap.Height; row++)
            {
                for (int col = 0; col < sourceBitmap.Width; col++)
                {
                    int sourceXY = row*sourceData.Stride + col*4;

                    sourcePoint.X = col;
                    sourcePoint.Y = row;

                    if (sourceXY >= 0 && sourceXY + 3 < pixelBuffer.Length)
                    {
                        var rotateXY = RotateXY(sourcePoint, degrees, xOffset, yOffset);

                        //Calculate Blue Rotation 
                        var resultXY = (int)(Math.Round((rotateXY.Y * sourceData.Stride) + (rotateXY.X * 4.0)));

                        if (imageBounds.Contains(rotateXY) &&
                            resultXY >= 0)
                        {
                            if (resultXY + 6 < resultBuffer.Length)
                            {
                                resultBuffer[resultXY + 4] = pixelBuffer[sourceXY];

                                resultBuffer[resultXY + 7] = 255;
                            }


                            if (resultXY + 3 < resultBuffer.Length)
                            {
                                resultBuffer[resultXY] = pixelBuffer[sourceXY];

                                resultBuffer[resultXY + 3] = 255;
                            }
                        }


                        //Calculate Green Rotation 
                        resultXY = (int) (Math.Round((rotateXY.Y*sourceData.Stride) + (rotateXY.X*4.0)));

                        if (imageBounds.Contains(rotateXY) && resultXY >= 0)
                        {
                            if (resultXY + 6 < resultBuffer.Length)
                            {
                                resultBuffer[resultXY + 5] = pixelBuffer[sourceXY + 1];

                                resultBuffer[resultXY + 7] = 255;
                            }


                            if (resultXY + 3 < resultBuffer.Length)
                            {
                                resultBuffer[resultXY + 1] = pixelBuffer[sourceXY + 1];

                                resultBuffer[resultXY + 3] = 255;
                            }
                        }


                        //Calculate Red Rotation 
                        resultXY = (int) (Math.Round((rotateXY.Y*sourceData.Stride) + (rotateXY.X*4.0)));


                        if (imageBounds.Contains(rotateXY) && resultXY >= 0)
                        {
                            if (resultXY + 6 < resultBuffer.Length)
                            {
                                resultBuffer[resultXY + 6] = pixelBuffer[sourceXY + 2];

                                resultBuffer[resultXY + 7] = 255;
                            }


                            if (resultXY + 3 < resultBuffer.Length)
                            {
                                resultBuffer[resultXY + 2] = pixelBuffer[sourceXY + 2];

                                resultBuffer[resultXY + 3] = 255;
                            }
                        }
                    }
                }
            }


            var resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData =
                resultBitmap.LockBits(new Rectangle(0, 0,
                    resultBitmap.Width, resultBitmap.Height),
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);


            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                resultBuffer.Length);


            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }

        public static Point RotateXY(Point source, double degrees, int offsetX, int offsetY)
        {
            int x = source.X - offsetX;
            int y = source.Y - offsetY;

            var result = new Point
            {
                X = (int) (Math.Round(x*Math.Cos(degrees) - y*Math.Sin(degrees))) + offsetX,
                Y = (int) (Math.Round(x*Math.Sin(degrees) + y*Math.Cos(degrees))) + offsetY
            };

            return result;
        }
    }
}