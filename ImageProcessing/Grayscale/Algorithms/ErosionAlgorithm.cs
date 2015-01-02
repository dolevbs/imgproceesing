using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class ErosionAlgorithm : UseBlackAndWhiteAlgorithm
    {
        private readonly CheckBoxCell[,] _structuringElement;

        public ErosionAlgorithm(BlackAndWhiteAlgorithm bitmapSource, CheckBoxCell[,] structuringElement) : base(bitmapSource)
        {
            _structuringElement = structuringElement;
        }

        public override void Execute()
        {
            
        }
    }
}