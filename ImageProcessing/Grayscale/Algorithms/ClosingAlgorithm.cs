using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class ClosingAlgorithm : UseBlackAndWhiteAlgorithm
    {
        private readonly CheckBoxCell[,] _structuringElement;

        public ClosingAlgorithm(BlackAndWhiteAlgorithm bitmapSource, CheckBoxCell[,] structuringElement)
            : base(bitmapSource)
        {
            _structuringElement = structuringElement;
        }

        public override void Execute()
        {
        }
    }
}