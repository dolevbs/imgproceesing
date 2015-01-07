using System.Drawing;
using Grayscale.AlgorithmsBase;

namespace Grayscale.Algorithms
{
    public class OpeningAlgorithm : UseBlackAndWhiteAlgorithm
    {
        private readonly bool?[,] _structuringElement;
        private readonly Point _target;
        private ErosionAlgorithm erosionAlg;
        private DilationAlgorithm dilationAlg;

        public OpeningAlgorithm(BlackAndWhiteAlgorithm bitmapSource, bool?[,] structuringElement, Point target)
            : base(bitmapSource)
        {
            _structuringElement = structuringElement;
            _target = target;
            erosionAlg = new ErosionAlgorithm(bitmapSource, _structuringElement, _target);            
        }

        public override void Execute()
        {
            erosionAlg.Execute();
            
            dilationAlg = new DilationAlgorithm(new BlackAndWhiteAlgorithm( new GrayScaleAlgorithm(
                            new SimpleBitmapSource(erosionAlg.Result))), _structuringElement, _target);
            dilationAlg.Execute();
            this.Result = dilationAlg.Result;

        }
    }
}