namespace Tetris.ApiClient.Interfaces
{
    public interface ITetrisGameResult<TWeights> : ITetrisAlgorithmT<TWeights>
    {
        int Fitness { get; set; }
        int Blocks { get; set; }
        int Rows { get; set; }
        int OneRowClearings { get; set; }
        int DoubleRowClearings { get; set; }
        int TripleRowClearings { get; set; }
        int QuadRowClearings { get; set; }
    }
}