namespace Tetris.ApiClient.Entities
{
    using Tetris.ApiClient.Interfaces;

    public class TetrisAlgorithmT<TWeights> : ITetrisAlgorithmT<TWeights>
    {
        public string Name { get; set; }
        public string AlgorithmId { get; set; }
        public TWeights Weights { get; set; }
    }
}
