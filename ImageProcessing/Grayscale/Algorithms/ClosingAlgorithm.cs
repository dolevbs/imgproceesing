using System.Drawing;
using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class ClosingAlgorithm : UseBlackAndWhiteAlgorithm
    {
        private readonly bool?[,] _structuringElement;
        private readonly Point _target;

        public ClosingAlgorithm(BlackAndWhiteAlgorithm bitmapSource, bool?[,] structuringElement, Point target)
            : base(bitmapSource)
        {
            _structuringElement = structuringElement;
            _target = target;
        }

        public override void Execute()
        {
        }
    }
}