using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class OpeningAlgorithm : UseBlackAndWhiteAlgorithm
    {
        private readonly CheckBoxCell[,] _structuringElement;

        public OpeningAlgorithm(BlackAndWhiteAlgorithm bitmapSource, CheckBoxCell[,] structuringElement)
            : base(bitmapSource)
        {
            _structuringElement = structuringElement;
        }

        public override void Execute()
        {
        }
    }
}