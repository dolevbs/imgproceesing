using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class DilationAlgorithm : UseBlackAndWhiteAlgorithm
    {
        private readonly CheckBoxCell[,] _structuringElement;

        public DilationAlgorithm(BlackAndWhiteAlgorithm bitmapSource, CheckBoxCell[,] structuringElement)
            : base(bitmapSource)
        {
            _structuringElement = structuringElement;
        }

        public override void Execute()
        {
        }
    }
}