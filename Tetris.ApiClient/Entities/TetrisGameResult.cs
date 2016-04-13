namespace Tetris.ApiClient.Entities
{
    using Tetris.ApiClient.Interfaces;

    public class TetrisGameResult<TWeights> : ITetrisGameResult<TWeights>
    {
        public TetrisGameResult(TetrisAlgorithmSetting<TWeights> setting)
        {
            this.Weights = setting.Weights;
            this.Name = setting.Name;
            this.AlgorithmId = setting.AlgorithmId;
            this.Weights = setting.Weights;
        }

        public string Name { get; set; }
        public string AlgorithmId { get; set; }
        public TWeights Weights { get; set; }
        public int Fitness { get; set; }
        public int Blocks { get; set; }
        public int Rows { get; set; }
        public int OneRowClearings { get; set; }
        public int DoubleRowClearings { get; set; }
        public int TripleRowClearings { get; set; }
        public int QuadRowClearings { get; set; }
    }
}