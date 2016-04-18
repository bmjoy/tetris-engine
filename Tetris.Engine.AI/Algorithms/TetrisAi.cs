namespace Tetris.Engine.AI.Algorithms
{
    using Tetris.Engine.AI.Algorithms.Weights;

    public class TetrisAi : IAlgorithm
    {
        private readonly float landingHeight;
        private readonly float rowTransitions;
        private readonly float columnTransitions;
        private readonly float numberOfHoles;
        private readonly float wellSums;
        private readonly float rowsCleared;

        public TetrisAi(TetrisAiWeights tetrisAiWeights)
        {
            this.landingHeight = tetrisAiWeights.LandingHeight;
            this.rowTransitions = tetrisAiWeights.RowTransitions;
            this.columnTransitions = tetrisAiWeights.ColumnTransitions;
            this.rowsCleared = tetrisAiWeights.RowsCleared;
            this.numberOfHoles = tetrisAiWeights.NumberOfHoles;
            this.wellSums = tetrisAiWeights.WellSums;
        }

        public float CalculateFitness(bool[][] gameBoard, Block previousBlock, int rowsCleared)
        {
            var features = new Features(gameBoard);
            var fitness = 0f;

            fitness += rowsCleared * this.rowsCleared;
            fitness += features.LandingHeight(previousBlock) * this.landingHeight;
            fitness += features.ColumnTransitions() * this.columnTransitions;
            fitness += features.NumberOfHoles() * this.numberOfHoles;
            fitness += features.RowTransitions() * this.rowTransitions;
            fitness += features.WellSums() * this.wellSums;

            return fitness;
        }
    }
}
