namespace Tetris.Engine.AI.Algorithms.Weights
{
    public class TetrisAiWeights
    {
        public float LandingHeight { get; set; }
        public float RowTransitions { get; set; }
        public float ColumnTransitions { get; set; }
        public float NumberOfHoles { get; set; }
        public float WellSums { get; set; }
        public float RowsCleared { get; set; }
    }
}
