namespace Tetris.ApiClient.Interfaces
{
    public interface ITetrisAlgorithmT<TWeights> : ITetrisAlgorithm
    {
        TWeights Weights { get; set; }
    }
}