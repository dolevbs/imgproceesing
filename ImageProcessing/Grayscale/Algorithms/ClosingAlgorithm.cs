using System.Drawing;
using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class ClosingAlgorithm : UseBlackAndWhiteAlgorithm
    {
        private readonly bool?[,] _structuringElement;
        private readonly Point _target;
        private ErosionAlgorithm erosionAlg;
        private DilationAlgorithm dilationAlg;

        public ClosingAlgorithm(BlackAndWhiteAlgorithm bitmapSource, bool?[,] structuringElement, Point target)
            : base(bitmapSource)
        {
            _structuringElement = structuringElement;
            _target = target;
            dilationAlg = new DilationAlgorithm(bitmapSource, _structuringElement, _target);
        }

        public override void Execute()
        {
            dilationAlg.Execute();

            erosionAlg = new ErosionAlgorithm(new BlackAndWhiteAlgorithm(new GrayScaleAlgorithm(
                            new SimpleBitmapSource(dilationAlg.Result))), _structuringElement, _target);
            erosionAlg.Execute();
            this.Result = erosionAlg.Result;

        }
    }
}