namespace Tetris.ApiClient.Entities
{
    using Tetris.ApiClient.Interfaces;

    public class TetrisAlgorithmSetting<TWeights> : ITetrisAlgorithmT<TWeights>
    {
        public string Name { get; set; }
        public TWeights Weights { get; set; }
        public int EvolutionNumber { get; set; }
        public int OverallAvgFitness { get; set; }
        public int BestFitness { get; set; }
        public int EvolutionFitness { get; set; }
        public string AlgorithmId { get; set; }
        public bool Active { get; set; }
    }
}