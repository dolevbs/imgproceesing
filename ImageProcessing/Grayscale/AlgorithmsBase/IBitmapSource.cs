using System.Drawing;

namespace Grayscale.AlgorithmsBase
{
    public interface IBlackAndWhiteBitmapSource : IGrayScaleBitmapSource
    {
        
    }

    public interface IGrayScaleBitmapSource : IBitmapSource
    {
         
    }

    public interface IBitmapSource
    {
        Bitmap Result { get; }
    }
}