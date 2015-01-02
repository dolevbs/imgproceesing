using Grayscale.Algorithms;

namespace Grayscale.AlgorithmsBase
{
    public abstract class UseBlackAndWhiteAlgorithm : BitmapAlgorithm
    {
        protected UseBlackAndWhiteAlgorithm(BlackAndWhiteAlgorithm bitmapSource)
            : base(bitmapSource)
        {
        }
    }
}