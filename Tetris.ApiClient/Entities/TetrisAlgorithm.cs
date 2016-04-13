namespace Tetris.ApiClient.Entities
{
    using Tetris.ApiClient.Interfaces;

    public class TetrisAlgorithm : ITetrisAlgorithm
    {
        public string Name { get; set; }
        public string AlgorithmId { get; set; }
    }
}